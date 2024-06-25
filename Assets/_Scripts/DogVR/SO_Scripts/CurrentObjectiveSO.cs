using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace DogVR
    {
    [CreateAssetMenu(menuName = "Variables/Current Objective")]
    public class CurrentObjectiveSO : ScriptableObject
        {
        public GameObject currentObjective;
        public string objectiveName;
        public bool objectiveComplete;

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

        public string ObjectiveName
            {
            get
                {
                return objectiveName;
                }
            set
                {
                objectiveName = value;
                }
            }

        public bool ObjectiveComplete
            {
            get
                {
                return objectiveComplete;
                }
            set
                {
                objectiveComplete = value;
                }
            }
        }
    }
