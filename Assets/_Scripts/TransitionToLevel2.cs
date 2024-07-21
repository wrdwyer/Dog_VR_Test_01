using EasyTransition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix;
using Sirenix.OdinInspector;
using DogVR;
using Unity.XR.CoreUtils;
using System;
using FMODUnity;

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
    [SerializeField]
    private float faderWaitTime = 2.0f; // Time to wait when fully faded to black
    [SerializeField]
    private SceneFader sceneFader;
    [SerializeField]
    private StudioEventEmitter stopCityEnviromentSound;
    [SerializeField]
    private StudioEventEmitter startFramEnviromentSound;

    private void Start()
        {
        ///sceneFader = FindAnyObjectByType<SceneFader>();
        }


    private void OnTriggerEnter(Collider other)
        {
        if (sceneFader != null)
            {
            sceneFader.gameObject.SetActive(true);
            
            StartCoroutine(FadeSequence());
            }

        }

    private IEnumerator FadeSequence()
        {
        // Fade to black
        yield return StartCoroutine(sceneFader.FadeIn());
        SetPlayersNewLocation();      
        
        // Wait for the specified time
        yield return new WaitForSeconds(faderWaitTime);

        // Fade back in
        yield return StartCoroutine(sceneFader.FadeOut());
        }

    private void SetPlayersNewLocation()
        {
        stopCityEnviromentSound.enabled = false;
        startFramEnviromentSound.enabled = true;
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

        Debug.Log("Transitioned TO lEVEL 2");
        }
    }
