using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DogVR
    {
    [CreateAssetMenu(menuName = "Variables/Current Objective")]
    public class CurrentObjectiveSO : ScriptableObject
        {
        public GameObject currentObjective;

        public GameObject CurrentObjective
            {
            get 
                {
                return currentObjective;
                }

            set 
                {
                currentObjective = value;
                }}
        }
    }
