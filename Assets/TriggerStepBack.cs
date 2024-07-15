using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Splines;


namespace DogVR.Actions
    {
    public class TriggerStepBack : MonoBehaviour
        {
        private Animator animator;
        [SerializeField]
        private string animationToTrigger = "StepBack";
        [SerializeField]
        private SplineAnimate splineToAnimate = null;
        public bool inRangeOfDog = false;        
        public bool triggeredEvent = false;
        public StudioEventEmitter audioToTrigger = null;


        private void Awake()
            {
            animator = GetComponentInParent<Animator>();
            }

        private void OnTriggerEnter(Collider other)
            {
            if (other.gameObject.CompareTag("Player"))
                {
                Debug.Log("Dog In Zone!!!!");
                inRangeOfDog = true;
                }
            }

        private void OnTriggerExit(Collider other)
            {
            if (other.gameObject.CompareTag("Player"))
                {
                Debug.Log("Dog Out of Zone!!!!");
                inRangeOfDog = false;
                }
            }

        // Start is called before the first frame update
        private void Start()
            {
            if (EventManager.Instance != null)
                {
                EventManager.Instance.barkTriggered += TriggerAnim;
                }

            }
        private void OnDisable()
            {
            if (EventManager.Instance != null)
                {
                EventManager.Instance.barkTriggered -= TriggerAnim;
                }
            }

        public void TriggerAnim()
            {
            if (inRangeOfDog)
                {
                if (GetComponentInParent<NavMeshAgent>() != null)
                    {
                    GetComponentInParent<NavMeshAgent>().enabled = false;
                    }
                if (GetComponent<SplineAnimate>() != null)
                    {
                    splineToAnimate.Play();
                    Debug.Log("Spline Animating");
                    }
                if (audioToTrigger != null)
                    {
                    audioToTrigger.Play();
                    Debug.Log("Audio Triggered");
                    }
                animator.Play(animationToTrigger);
                triggeredEvent = true;
                Debug.Log("Animation Triggered");

                }


            }
        public void EnableNavMesh()
            {
            if (GetComponent<NavMeshAgent>() != null)
                {
                GetComponent<NavMeshAgent>().enabled = true;
                }
            }
        }
    }
