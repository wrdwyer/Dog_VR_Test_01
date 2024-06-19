using Sirenix.OdinInspector;
using UnityEngine;

namespace DogVR
    {
    public class GameManager : MonoBehaviour
        {
        public static GameManager Instance { get; private set; }

        public GameObject playerPrefab;
        [SerializeField]
        private GameObject currentObjective;
        public PlayerGameObjectSO playerGameObjectSO; // Reference to the object you want to access

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
            }
        // Property for CurrentObjective with getter and setter
        public GameObject CurrentObjective
            {
            get { return currentObjective; }
            set { currentObjective = value; }
            }

        }

    }