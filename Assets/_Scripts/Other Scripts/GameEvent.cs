using System.Collections.Generic;
using UnityEngine;

namespace Unity6Test
    {
    [CreateAssetMenu(menuName = "Events/New Game Event")]
    public class GameEvent : ScriptableObject
        {
        [SerializeField] private List<GameEventListener> listeners = new List<GameEventListener>();
        public void TriggerEvent()
            {
            for (int i = listeners.Count - 1; i >= 0; i--)
                {
                listeners[i].OnEventTriggered();
                }
            }

        public void AddListener(GameEventListener listener)
            {
            listeners.Add(listener);
            }

        public void RemoveListener(GameEventListener listener)
            {
            listeners.Remove(listener);
            }
        }
    }
