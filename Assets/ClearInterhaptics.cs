using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Interhaptics.Core;
using Sirenix.OdinInspector;

public class ClearInterhaptics : MonoBehaviour
    {

    [Button("Clear Interhaptics")]
    public void QuitInterhaptics()// Call this method before exiting the application
        {
        Debug.Log("Clearing Interhaptics");
        //HAR.Quit();
        }
}
