using DogVR;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class StartAnimationTruckToFarm : MonoBehaviour
    {
    [SerializeField]
    private SplineContainer truckSpline;
    [SerializeField]
    private SplineAnimate splineAnimate;
    [SerializeField]
    private Transform snapVolume;
    private GameObject player = null;

    private void OnEnable()
        {
        GameManager.Instance.playerGameObjectSO.persistentObject.transform.SetParent(snapVolume.transform, true);
        }
    void Awake()
        {        
       
        }

    void Start()
        {
        //GameManager.Instance.playerGameObjectSO.persistentObject.transform.parent.SetParent(snapVolume.transform, true);
        StartTruckAnimation();   
        }

    IEnumerator WaitForReverseAnimation()
        {
        // Wait for the tailgate animation to complete
        yield return new WaitUntil(() => splineAnimate.ElapsedTime <= splineAnimate.Duration);

        // Call the method once the animation is complete
        FinishTruckAnimation();
        }

    private void FinishTruckAnimation()
        {
        //player.transform.SetParent(null);
        Debug.Log("FinishTruckAnimation");
        }

    private void StartTruckAnimation()
        {
        splineAnimate.Container = truckSpline;
        splineAnimate.enabled = true;
        }
    }
