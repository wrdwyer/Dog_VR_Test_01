using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DogVR
    {
    public class DisableTeleportComponents : MonoBehaviour
        {
        // List of components to disable
        public Behaviour[] componentsToDisable;

        void OnEnable()
            {
            DisableComponents();
            }

        void OnDisable()
            {
            EnableComponents();
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
