using Core_Scripts.SOSingletons;
using GameScripts.GameEvent;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

namespace Core_Scripts.PlayerSoundSystem {
    public class TileReader : MonoBehaviour {
        [SerializeField] private Grid _tileMapsGrid;
        [SerializeField] private SOVec2Singleton _directionToLook;
        [SerializeField] private SOBaseGameEvent _eventWhenToLook;
        public UnityEvent<string> TileRedEvent;
        private Tilemap[] _tilemapsArray;

        private void Awake() {
            _tilemapsArray = _tileMapsGrid.GetComponentsInChildren<Tilemap>();
            _eventWhenToLook.Subscribe(GetTileAtSingletonPosition);
        }

        public void GetTileAtSingletonPosition() {
            foreach (var tilemap in _tilemapsArray) {
                var gridPos = tilemap.WorldToCell(_directionToLook.Value);
                var tile = tilemap.GetTile(gridPos);

                if (tile != null) {
                    print(tile.name);
                    TileRedEvent.Invoke(tile.name);
                    return;
                }
            }
        }
        
    }
}