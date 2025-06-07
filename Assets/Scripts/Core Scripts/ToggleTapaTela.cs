using GameScripts.GameEvent;
using UnityEngine;

namespace Core_Scripts {
    public class ToggleTapaTela : MonoBehaviour {
        [SerializeField] private SOBaseGameEvent _toggleTapaEvent;
        private GameObject _tapaTelaGameobj;

        private void Awake() {
            _tapaTelaGameobj = transform.GetChild(0).gameObject;
        }

        private void OnEnable() {
            _toggleTapaEvent.Subscribe(ToggleIt);
        }

        private void OnDisable() {
            _toggleTapaEvent.Unsubscribe(ToggleIt);
        }

        public void ToggleIt() {
            _tapaTelaGameobj.SetActive(!_tapaTelaGameobj.activeSelf);
        }
    }
}