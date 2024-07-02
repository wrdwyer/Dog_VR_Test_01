using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DogVR
    {
    public class StartGame : MonoBehaviour
        {
        [SerializeField]
        private StateManager stateManager;
        [SerializeField]
        private GameObject gameUI;

        private void Start()
            {
            stateManager = GetComponent<StateManager>();
            }

        public void LoadLevelOne()
            {
            stateManager.UpdateGameState(StateManager.GameState.Playing);
            gameUI.SetActive(false);
            Debug.Log("Loading Level One");
            }
        }


    }
