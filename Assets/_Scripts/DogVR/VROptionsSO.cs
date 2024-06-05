using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DogVR
    {
    [ CreateAssetMenu(fileName = "VROptionsSO", menuName = "DogVR/VROptionsSO", order = 1)]
    public class VROptionsSO : ScriptableObject
        {
        public bool enableDogVision = true;
        public bool enableVignette = true;
        public bool snapTurn = false;

        }
    }



