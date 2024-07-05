using DogVR;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class StateManager : MonoBehaviour
    {
    [SerializeField]
    private SceneManager sceneManager;
    [SerializeField]
    private Transform startLocation;
    private DynamicMoveProvider dynamicMoveProvider;


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
                //sceneManager.Start();
                GameManager.Instance.SetObjectivesManager.CurrentObjectiveIndex = 0;
                SetPlayerStartLocation();
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

    private void SetPlayerStartLocation()
        {
        GameManager.Instance.playerGameObjectSO.persistentObject.transform.SetPositionAndRotation(startLocation.position, startLocation.rotation);
        // Access the DynamicMoveProvider component on the GameObject
        DynamicMoveProvider dynamicMoveProvider = GameManager.Instance.playerGameObjectSO.persistentObject.GetComponentInChildren<DynamicMoveProvider>();

        // Check if the DynamicMoveProvider component is not null
        if (dynamicMoveProvider != null)
            {
            // Enable the DynamicMoveProvider component
            dynamicMoveProvider.enabled = true;
            }
        /*GameManager.Instance.playerGameObjectSO.persistentObject.transform.SetLocalPositionAndRotation(new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
        
        
        GameManager.Instance.playerGameObjectSO.persistentObject.GetComponent<DynamicMoveProvider>().enabled = true;
        XROrigin xrOrigin = FindFirstObjectByType<XROrigin>();
        if (xrOrigin == null)
            {
            Debug.LogError("No XROrigin found in the scene");
            return;
            }
        xrOrigin.gameObject.transform.localPosition = Vector3.zero;

        Debug.Log("Transitioned");*/
        }
    }
