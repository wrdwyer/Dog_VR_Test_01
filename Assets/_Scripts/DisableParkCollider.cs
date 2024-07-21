using DogVR.Actions;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DisableParkCollider : MonoBehaviour
    {

    [SerializeField]
    private BoxCollider boxColliderToDisable;
    [SerializeField]
    private GameObject[] objectToCheckStepBack;

    private void Update()
        {
        foreach (GameObject obj in objectToCheckStepBack)
            {

            if (!obj.GetComponentInChildren<TriggerStepBack>().triggeredEvent)
                {

                break;
                }
            else
                {
                DisableParkColliderWall();
                }
            }
        }



    public void DisableParkColliderWall()
        {            
        boxColliderToDisable.enabled = false;
        Debug.Log("Disabled Collider by Bark");
        gameObject.SetActive(false);
        }

    }
