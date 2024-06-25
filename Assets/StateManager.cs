using DogVR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
    {

    public enum GameState
        {
        Initializing,
        Playing,
        Paused,
        GameOver
        }
    private GameState currentState;

    private void Start()
        {
        UpdateGameState(GameState.Initializing);
        }

    public void UpdateGameState(GameState newState)
        {
        currentState = newState;
        switch (currentState)
            {
            case GameState.Initializing:
                Debug.Log("Initializing");
                GameManager.Instance.SetObjectivesManager.CurrentObjectiveIndex = 0;

                break;
            case GameState.Playing:
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
