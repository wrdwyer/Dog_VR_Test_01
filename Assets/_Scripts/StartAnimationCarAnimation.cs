
using System.Collections;

using UnityEngine;
using DogVR;

using UnityEngine.Splines;

using FMODUnity;

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

    void Awake()
        {

        if (tailGateanimator == null)
            {
            tailGateanimator = GetComponent<Animator>();
            }
        }

    void Start()
        {
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
                StartCoroutine(WaitForAnimation());
                GetComponent<DisableXRRigMovement>().DisableMovement();
                if (GameManager.Instance.playerGameObjectSO.persistentObject.GetComponentInChildren<DisableTeleportComponents>() != null)
                    {
                    GameManager.Instance.playerGameObjectSO.persistentObject.GetComponentInChildren<DisableTeleportComponents>().enabled = true;
                    
                    }
                else
                    {
                    Debug.Log("DisableTeleportComponents not found");
                    }
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
