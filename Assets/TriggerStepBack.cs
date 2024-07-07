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
        private string triggerName = "StepBack";
        [SerializeField]
        private SplineAnimate splineAnimate = null;
        private bool inRangeOfDog = false;


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
                    splineAnimate.Play();
                    Debug.Log("Spline Animating");
                    }
                animator.Play(triggerName);
                Debug.Log("Triggering Flying");

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
