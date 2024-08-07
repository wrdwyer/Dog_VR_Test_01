using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

namespace DogVR
    {
    public class SceneManager : MonoBehaviour
        {
        public Transform startLocation; // Reference to the GameObject that specifies the start location
        public Transform parentLocation;
        public void Start()
            {
            if (GameManager.Instance != null && GameManager.Instance.playerGameObjectSO != null)
                {
                // Get the prefab from the ScriptableObject
                //-GameObject prefab = GameManager.Instance.playerGameObjectSO.persistentObject;
                GameObject prefab = GameManager.Instance.playerPrefab;

                if (prefab != null && startLocation != null)
                    {
                    // Instantiate the prefab at the start location's position and rotation
                    //-GameObject instantiatedPlayerObject = Instantiate(prefab, startLocation.position, startLocation.rotation, parentLocation);
                    //-instantiatedPlayerObject.SetActive(true);

                    // Store the reference in the ScriptableObject for future use if needed
                    //-GameManager.Instance.playerGameObjectSO.persistentObject = instantiatedPlayerObject;
                    GameManager.Instance.playerGameObjectSO.persistentObject = prefab;
                    GameManager.Instance.playerGameObjectSO.persistentObject.transform.SetPositionAndRotation(startLocation.position, startLocation.rotation);
                    /*DisableTeleportComponents disableTeleportComponents = GameManager.Instance.playerGameObjectSO.persistentObject.GetComponentInChildren<DisableTeleportComponents>();
                    if (disableTeleportComponents != null)
                        {
                        // Enable the DisableTeleportComponents component
                        disableTeleportComponents.enabled = true;
                        Debug.Log("Disable Teleport Components Enabled");
                        }
                    }*/
                    }
                }

            }


        }
    }
