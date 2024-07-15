using DogVR.Actions;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

namespace DogVR
    {
    public class GameManager : MonoBehaviour
        {
        public static GameManager Instance { get; private set; }

        public GameObject playerPrefab;
        public CurrentObjectiveSO currentObjectiveSO;
        public PlayerGameObjectSO playerGameObjectSO; // Reference to the object you want to access
        public SetObjectives SetObjectivesManager;
        public BonesCollectedSO BonesCollectedSO;
        public Volume VolumePostProcessingVolume;
       
        private void Awake()
            {
            if (Instance == null)
                {
                Instance = this;
                DontDestroyOnLoad(playerGameObjectSO); // Persist across scenes
                }
            else
                {
                Destroy(playerGameObjectSO);
                }

            playerGameObjectSO.persistentObject = playerPrefab;
            currentObjectiveSO.CurrentObjective = null;
            currentObjectiveSO.ObjectiveName = null;
            currentObjectiveSO.ObjectiveComplete = false;
            BonesCollectedSO.bonesCollected = 0;
            }

       // private void Start()
           // {
            /*playerGameObjectSO.persistentObject = playerPrefab;
            currentObjectiveSO.CurrentObjective = null;
            currentObjectiveSO.ObjectiveName = null;
            currentObjectiveSO.ObjectiveComplete = false;
            BonesCollectedSO.bonesCollected = 0;*/
           

            /*// Property for CurrentObjective with getter and setter
            public CurrentObjectiveSO CurrentObjective
                {
                get { return CurrentObjective; }
                set { CurrentObjective = value; }
                }*/

           // }

        /*private void OnDestroy()
            {
            Destroy(playerGameObjectSO);
            }*/

        }
    }