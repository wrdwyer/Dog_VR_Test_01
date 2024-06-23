using EasyTransition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix;
using Sirenix.OdinInspector;
using DogVR;
using Unity.XR.CoreUtils;

public class TransitionToLevel2 : MonoBehaviour
    {
    [SerializeField]
    private GameObject parentFarmTruckObject;
    [SerializeField]
    private string truckTag = "Truck";
    [SerializeField]
    private GameObject FarmTruckToFarm;
    [SerializeField]
    private Transform playerParent;
    [SerializeField]
    private Transform FarmTruckSnapVolume;


    private void OnTriggerEnter(Collider other)
        {
        parentFarmTruckObject.gameObject.SetActive(false);
        FarmTruckToFarm.SetActive(true);
        GameManager.Instance.playerGameObjectSO.persistentObject.transform.SetParent(FarmTruckSnapVolume.transform, true);        
        GameManager.Instance.playerGameObjectSO.persistentObject.transform.SetPositionAndRotation(FarmTruckSnapVolume.position, FarmTruckSnapVolume.rotation);
        GameManager.Instance.playerGameObjectSO.persistentObject.transform.SetLocalPositionAndRotation(new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
        XROrigin xrOrigin = FindFirstObjectByType<XROrigin>();
        if (xrOrigin == null)
            {
            Debug.LogError("No XROrigin found in the scene");
            return;
            }
        xrOrigin.gameObject.transform.localPosition = Vector3.zero;
        
        Debug.Log("Transitioned");
        Debug.Log(other);
        }
    }
