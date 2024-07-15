using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaitScript : MonoBehaviour
{
    public float waitTime = 3f; // Time to wait in seconds
    public UnityEvent onTimerComplete;

    private void Start()
        {
        StartCoroutine(WaitAndInvokeEvent());
        }

    private IEnumerator WaitAndInvokeEvent()
        {
        yield return new WaitForSeconds(waitTime);

        // Trigger the Unity Event when the wait time is complete
        onTimerComplete.Invoke();
        }
    }
