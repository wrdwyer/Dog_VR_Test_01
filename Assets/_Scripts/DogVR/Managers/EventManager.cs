using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DogVR
    {
    public class EventManager : MonoBehaviour
        {
        public static EventManager Instance;

        public delegate void BarkHandler();
        public event BarkHandler barkTriggered;

        private void Awake()
            {
            if (Instance == null)
                {
                Instance = this;
                DontDestroyOnLoad(gameObject); // Persist across scenes
                }
            else
                {
                Destroy(gameObject);
                }
            }

        public void Bark()
            {
            barkTriggered?.Invoke();
            }

        }
    }
