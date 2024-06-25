using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using DogVR;

public class TruckArrivesAtFarm : MonoBehaviour
    {
    [SerializeField]
    private Animator tailGateanimator;
    [SerializeField]
    private SplineAnimate splineAnimate;
    [SerializeField]
    private Transform playerParent;

    private void Start()
        {

        }
    private void Update()
        {
        splineAnimate.Completed += () =>
        {
            
            GameManager.Instance.playerGameObjectSO.persistentObject.transform.SetParent(playerParent, true);
            GameManager.Instance.playerGameObjectSO.persistentObject.GetComponentInChildren<DisableTeleportComponents>().enabled = false;
            Debug.Log("Truck arrives at farm");
            tailGateanimator.Play("OpenTailGate");
        };
        }
    }
