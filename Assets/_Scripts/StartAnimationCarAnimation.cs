
using System.Collections;

using UnityEngine;
using DogVR;

using UnityEngine.Splines;

using FMODUnity;
using Unity.XR.CoreUtils;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class StartAnimationCarAnimation : MonoBehaviour
    {

    [SerializeField]
    private Animator tailGateanimator;
    [SerializeField]
    private Animator animatruckReverse;
    [SerializeField]
    private GameObject farmerTruck;
    [SerializeField]
    private SplineContainer truckSpline;
    [SerializeField]
    private SplineAnimate splineAnimate;
    [SerializeField]
    private BoxCollider BoxCollider;
    [SerializeField]
    private BoxCollider ExitBoxCollider;
    [SerializeField]
    private Transform snapVoluelume;
    [SerializeField]
    private Transform playerParent;
    [SerializeField]
    private StudioEventEmitter startEngine;
    [SerializeField]
    private StudioEventEmitter WaitCopperAudio;

    private XROrigin m_XROrigin;
    private GameObject m_RightController;
    private GameObject m_LeftController;

    void Awake()
        {

        if (tailGateanimator == null)
            {
            tailGateanimator = GetComponent<Animator>();
            }
        }

    void Start()
        {
        m_XROrigin = GameManager.Instance.playerGameObjectSO.persistentObject.gameObject.GetComponentInChildren<XROrigin>();
        m_RightController = m_XROrigin.GetComponent<XRInputModalityManager>().rightController;
        m_LeftController = m_XROrigin.GetComponent<XRInputModalityManager>().leftController;
        //player = GameManager.Instance.playerGameObjectSO.persistentObject;
        //player = GameObject.FindWithTag("Player");
        }
     
    private void OnTriggerEnter(Collider other)
        {
        if (other.CompareTag("Player"))
            {
            if (tailGateanimator != null)
                {
                startEngine.Play();
                WaitCopperAudio.Play();
                tailGateanimator.Play("CloseTailGate");
                GameManager.Instance.playerGameObjectSO.persistentObject.transform.SetParent(snapVoluelume.transform, true);
                //other.transform.parent.SetParent(snapVoluelume.transform, true);
                m_RightController.GetComponent<ActionBasedControllerManager>().enabled = false;
                m_LeftController.GetComponent<ActionBasedControllerManager>().smoothMotionEnabled = false;
                StartCoroutine(WaitForAnimation());
                //GetComponent<DisableXRRigMovement>().DisableMovement();
                //if (GameManager.Instance.playerGameObjectSO.persistentObject.GetComponentInChildren<DisableTeleportComponents>() != null)
                //    {
                //    GameManager.Instance.playerGameObjectSO.persistentObject.GetComponentInChildren<DisableTeleportComponents>().enabled = true;
                    
                //    }
                //else
                //    {
                //    Debug.Log("DisableTeleportComponents not found");
                //    }
                //Need to reverse this when Sceane 2 is loaded and Truck has finished animations.
                }
            }
        }

    private void OnDisable()
        {
        if (farmerTruck != null)
            {
            if (GameManager.Instance.playerGameObjectSO.persistentObject != null) GameManager.Instance.playerGameObjectSO.persistentObject.transform.SetParent(playerParent,true);
            }
        }

    IEnumerator WaitForAnimation()
        {
        // Wait for the tailgate animation to complete
        yield return new WaitUntil(() => tailGateanimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);

        // Call the method once the animation is complete
        FinishedTailGateAnimation();
        }


    public void FinishedTailGateAnimation()
        {
        animatruckReverse.enabled = true;
        StartCoroutine(WaitForReverseAnimation());
        //animatruckReverse.Play("ReverseTruck");
        Debug.Log("TailGate Animation Finished");
        }

    IEnumerator WaitForReverseAnimation()
        {
        // Wait for the tailgate animation to complete
        yield return new WaitUntil(() => animatruckReverse.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);

        // Call the method once the animation is complete
        FinishedReverseTruckAnimation();
        }

    private void FinishedReverseTruckAnimation()
        {
        if (ExitBoxCollider != null)
            {
            ExitBoxCollider.enabled = false;
            }
        animatruckReverse.enabled = false;
        Debug.Log("Truck Animation Finished");
        splineAnimate.Container = truckSpline;
        splineAnimate.enabled = true;
        }
    }
