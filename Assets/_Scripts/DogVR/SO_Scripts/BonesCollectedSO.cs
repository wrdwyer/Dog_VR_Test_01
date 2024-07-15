using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DogVR
    {
    [CreateAssetMenu(fileName = "BonesCollectedSO", menuName = "DogVR/BonesCollectedSO", order = 5)]
    public class BonesCollectedSO : ScriptableObject
        {
        public int bonesCollected = 0;
        }
    }
