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
    private GameObject player;

    void Awake()
        {
        StartTruckAnimation();
        }

    void Start()
        {
        player = GameManager.Instance.playerGameObjectSO.persistentObject;
        player.transform.SetParent(this.transform, true);
        //player = GameObject.FindWithTag("Player");
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
        Debug.Log("FinishTruckAnimation");
        }

    private void StartTruckAnimation()
        {
        splineAnimate.Container = truckSpline;
        splineAnimate.enabled = true;
        }
    }
