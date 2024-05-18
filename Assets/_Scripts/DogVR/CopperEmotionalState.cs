using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DogVR
    {
    [CreateAssetMenu(menuName = "Variables/Copper Emotional State")]
    public class CopperEmotionalState : ScriptableObject
        {
        public enum AnimalState
            {
            Idle,
            Anxious,
            Happy,
            Sad,
            Threatened
            }

        public AnimalState currentState = AnimalState.Idle;
        
        }
    }
