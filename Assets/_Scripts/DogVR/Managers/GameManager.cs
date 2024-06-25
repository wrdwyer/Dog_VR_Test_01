using Sirenix.OdinInspector;
using UnityEngine;

namespace DogVR
    {
    public class GameManager : MonoBehaviour
        {
        public static GameManager Instance { get; private set; }

        public GameObject playerPrefab;
        public CurrentObjectiveSO currentObjectiveSO;
        public PlayerGameObjectSO playerGameObjectSO; // Reference to the object you want to access
        public SetObjectives SetObjectivesManager;
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
            }

        private void Start()
            {
            playerGameObjectSO.persistentObject = playerPrefab;
            currentObjectiveSO.CurrentObjective = null;
            currentObjectiveSO.ObjectiveName = null;
            currentObjectiveSO.ObjectiveComplete = false;
            }
        /*// Property for CurrentObjective with getter and setter
        public CurrentObjectiveSO CurrentObjective
            {
            get { return CurrentObjective; }
            set { CurrentObjective = value; }
            }*/

        }

    }