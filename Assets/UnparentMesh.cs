using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapBall : MonoBehaviour
    {
    [SerializeField] public GameObject objectToDisable;
    [SerializeField] public GameObject objectToEnable;

    public void Unparent()
        {
        if (objectToDisable != null && objectToEnable != null)
            {
            Debug.Log(objectToDisable.transform.position);
            objectToDisable.SetActive(false);
            objectToEnable.SetActive(true);
            }
           
        }
    }
