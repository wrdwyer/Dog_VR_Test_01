using System;
using UnityEngine;
using UnityEngine.Events;


namespace Unity6Test
    {
    [CreateAssetMenu(menuName = "Events/Int Event Channel")]
    public class IntEventChannelSO : ScriptableObject
        {
        public Action<int> OnEventRaised;

        public void RaiseEvent(int value)
            {
            OnEventRaised?.Invoke(value);
            }
        }
    }



