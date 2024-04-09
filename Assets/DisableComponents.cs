using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableComponents : MonoBehaviour
{
    // A function that disables all components attached to the game object except for the current component.
    private void OnEnable()
    {
        Component[] components = GetComponents<Component>();

        foreach (Component comp in components)
        {
            if (comp != this)
            {
                if (comp is Behaviour)
                {
                    ((Behaviour)comp).enabled = false;
                }
                else if (comp is Renderer)
                {
                    ((Renderer)comp).enabled = false;
                }
                // Add more specific component types here as needed
            }
        }
    }
}
