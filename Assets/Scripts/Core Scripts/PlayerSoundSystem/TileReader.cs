using System;
using Core_Scripts.SOSingletons;
using GameScripts.GameEvent;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

namespace Core_Scripts.PlayerSoundSystem {
    public class TileReader : MonoBehaviour {
        [SerializeField] private Grid _tileMapsGrid;
        [SerializeField] private SOVec3Singleton _directionToLook;
        [SerializeField] private SOBaseGameEvent _eventWhenToLook;
        public UnityEvent<string> TileRedEvent;
        private Tilemap[] _tilemapsArray;

        private void Awake() {
            _tilemapsArray = _tileMapsGrid.GetComponentsInChildren<Tilemap>();
        }

        private void OnEnable() {
            _eventWhenToLook.Subscribe(GetTileAtSingletonPosition);
        }

        private void OnDisable() {
            _eventWhenToLook.Unsubscribe(GetTileAtSingletonPosition);
        }

        public void GetTileAtSingletonPosition() {
            foreach (var tilemap in _tilemapsArray) {
                var gridPos = tilemap.WorldToCell(_directionToLook.Value);
                var tile = tilemap.GetTile(gridPos);

                if (tile != null) {
                    TileRedEvent.Invoke(tile.name);
                    return;
                }
            }
        }
        
    }
}