using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using DogVR;
using FMODUnity;

public class TruckArrivesAtFarm : MonoBehaviour
    {
    [SerializeField]
    private Animator tailGateanimator;
    [SerializeField]
    private SplineAnimate splineAnimate;
    [SerializeField]
    private Transform playerParent;
    [SerializeField]
    private StudioEventEmitter stopTruckEmitter;

    private void Update()
        {
        splineAnimate.Completed += () =>
        {            
            GameManager.Instance.playerGameObjectSO.persistentObject.transform.SetParent(playerParent, true);
            //GameManager.Instance.playerGameObjectSO.persistentObject.GetComponentInChildren<DisableTeleportComponents>().enabled = false;
            Debug.Log("Truck arrives at farm");
            tailGateanimator.Play("OpenTailGate");
            stopTruckEmitter.Stop();
            GetComponent<EnableXRRigMovement>().EnableMovement();
        };
        }
    }
