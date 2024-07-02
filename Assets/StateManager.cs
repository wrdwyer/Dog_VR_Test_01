using DogVR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
    {
    [SerializeField]
    private SceneManager sceneManager;
    public enum GameState
        {
        MainMenu,
        Playing,
        Paused,
        GameOver
        }
    private GameState currentState;

    private void Start()
        {
        UpdateGameState(GameState.MainMenu);
        //sceneManager = GetComponent<SceneManager>();
        }

    public void UpdateGameState(GameState newState)
        {
        currentState = newState;
        switch (currentState)
            {
            case GameState.MainMenu:
                Debug.Log("Main Menu");
                break;
            case GameState.Playing:
                sceneManager.CreatePlayer();
                GameManager.Instance.SetObjectivesManager.CurrentObjectiveIndex = 0;
                Debug.Log("Playing");
                break;
            case GameState.Paused:
                Debug.Log("Paused");
                break;
            case GameState.GameOver:
                Debug.Log("GameOver");
                break;
            }
        }
    }
