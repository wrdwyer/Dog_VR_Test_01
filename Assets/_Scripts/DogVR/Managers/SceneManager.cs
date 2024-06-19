using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

namespace DogVR
    {
    public class SceneManager : MonoBehaviour
        {
        public Transform startLocation; // Reference to the GameObject that specifies the start location
        
        void Start()
            {
            if (GameManager.Instance != null && GameManager.Instance.playerGameObjectSO != null)
                {
                // Get the prefab from the ScriptableObject
                GameObject prefab = GameManager.Instance.playerGameObjectSO.persistentObject;

                if (prefab != null && startLocation != null)
                    {
                    // Instantiate the prefab at the start location's position and rotation
                    GameObject instantiatedPlayerObject = Instantiate(prefab, startLocation.position, startLocation.rotation);

                    // Store the reference in the ScriptableObject for future use if needed
                    GameManager.Instance.playerGameObjectSO.persistentObject = instantiatedPlayerObject;
                    }
                }
            }
     
        }

      
    }
