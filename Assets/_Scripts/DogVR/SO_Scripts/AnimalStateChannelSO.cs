using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DogVR
    {
    [CreateAssetMenu(menuName = "Events/Animal State Channel")]
    public class AnimalStateChannelSO : ScriptableObject
        {
        public UnityAction<CopperEmotionalState> OnEventRaised;

        public void RaiseEvent(CopperEmotionalState state)
            {
            OnEventRaised?.Invoke(state);
            }

        }
    }
