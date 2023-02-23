using Core_Scripts.SOSingletons;
using GameScripts.GameEvent;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Core_Scripts.PlayerSoundSystem {
    public class TileReader : MonoBehaviour {
        [SerializeField] private Grid _tileMapsGrid;
        [SerializeField] private SOVec2Singleton _directionToLook;
        [SerializeField] private SOBaseGameEvent _eventWhenToLook;
        private Tilemap[] _tilemapsArray;

        private void Awake() {
            _tilemapsArray = _tileMapsGrid.GetComponentsInChildren<Tilemap>();
        }
        
        
        
    }
}