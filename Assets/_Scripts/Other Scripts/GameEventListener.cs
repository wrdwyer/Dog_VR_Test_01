using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

namespace Unity6Test
    {
    public class GameEventListener : MonoBehaviour
        {
        public GameEvent gameEvent;
        public UnityEvent onEventTriggered;

        private void OnEnable()
            {
            gameEvent.AddListener(this);
            }

        private void OnDisable()
            {
            gameEvent.RemoveListener(this);
            }
        [Button("Trigger Event")]
        public void OnEventTriggered()
            {
            onEventTriggered.Invoke();
            }
        }
    }
