using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core_Scripts {
    public class PlayerSpawnPointShuffle : MonoBehaviour {
        private Transform[] _possibleSpawnPointsArray;
        private GameObject _player;

        private void Awake() {
            _player = GameObject.FindGameObjectWithTag("Player");
            _possibleSpawnPointsArray = GetComponentsInChildren<Transform>();
            
            _player.transform.position = _possibleSpawnPointsArray[Random.Range(1, _possibleSpawnPointsArray.Length)].position; //Começa em 1 pq se não ele vai pegar a posição dele proprio
        }
    }
}