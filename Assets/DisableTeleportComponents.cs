using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DogVR
    {
    public class DisableTeleportComponents : MonoBehaviour
        {
        // List of components to disable
        public Behaviour[] componentsToDisable;
        public GameObject[] gameObjectsToDisable;
        void OnEnable()
            {
            DisableComponents();
            DisableGameObjects();
            }

        void OnDisable()
            {
            EnableComponents();
            EnableGameObjects();
            }

        // Method to disable the components
        void DisableComponents()
            {
            foreach (var component in componentsToDisable)
                {
                if (component != null)
                    {
                    component.enabled = false;
                    }
                }
            }

        // Method to disable the game objects
        void DisableGameObjects()
            {
            foreach (var gameObject in gameObjectsToDisable)
                {
                if (gameObject != null)
                    {
                    gameObject.SetActive(false);
                    }
                }
            }

        // Method to enable the game objects
        void EnableGameObjects()
            {
            foreach (var gameObject in gameObjectsToDisable)
                {
                if (gameObject != null)
                    {
                    gameObject.SetActive(true);
                    }
                }
            }

        // Method to enable the components
        void EnableComponents()
            {
            foreach (var component in componentsToDisable)
                {
                if (component != null)
                    {
                    component.enabled = true;
                    }
                }
            }

        }
    }
