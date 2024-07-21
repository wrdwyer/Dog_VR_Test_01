using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using DogVR;
using FMODUnity;
using Unity.XR.CoreUtils;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class TruckArrivesAtFarm : MonoBehaviour
    {
    [SerializeField]
    private Animator tailGateanimator;
    [SerializeField]
    private SplineAnimate splineAnimate;
    [SerializeField]
    private Transform playerParent;
    //[SerializeField]
    //private StudioEventEmitter stopTruckEmitter;
    private XROrigin m_XROrigin;
    private GameObject m_RightController;
    private GameObject m_LeftController;

    public string stopTruckEvent;
    private FMOD.Studio.EventInstance stopTruckEmitter;

 private void Start()
        {
        stopTruckEmitter = FMODUnity.RuntimeManager.CreateInstance(stopTruckEvent);
        stopTruckEmitter.start();
        m_XROrigin = GameManager.Instance.playerGameObjectSO.persistentObject.gameObject.GetComponentInChildren<XROrigin>();
        m_RightController = m_XROrigin.GetComponent<XRInputModalityManager>().rightController;
        m_LeftController = m_XROrigin.GetComponent<XRInputModalityManager>().leftController;
        m_RightController.GetComponent<ActionBasedControllerManager>().enabled = false;
        m_LeftController.GetComponent<ActionBasedControllerManager>().smoothMotionEnabled = false;
        }

    private void Update()
        {
        splineAnimate.Completed += () =>
        {            
            GameManager.Instance.playerGameObjectSO.persistentObject.transform.SetParent(playerParent, true);
            //GameManager.Instance.playerGameObjectSO.persistentObject.GetComponentInChildren<DisableTeleportComponents>().enabled = false;
            Debug.Log("Truck arrives at farm");
            tailGateanimator.Play("OpenTailGate");
            m_RightController.GetComponent<ActionBasedControllerManager>().enabled = true;
            m_LeftController.GetComponent<ActionBasedControllerManager>().smoothMotionEnabled = true;
            stopTruckEmitter.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            stopTruckEmitter.release();
            //stopTruckEmitter.Stop();
            GetComponent<EnableXRRigMovement>().EnableMovement();
        };
        }
    }
