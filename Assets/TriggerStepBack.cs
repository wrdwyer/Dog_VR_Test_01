using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace DogVR.Actions
    {
    public class TriggerStepBack : MonoBehaviour
        {
        private Animator animator;
        [SerializeField]
        private string triggerName = "StepBack";
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
        private void OnEnable()
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
                if (GetComponent<NavMeshAgent>()!=null)
                    {
                    GetComponent<NavMeshAgent>().enabled = false;
                    }
                animator.Play(triggerName);
                Debug.Log("Triggering step back");
                }
         
            
            }
        public void EnableNavMesh()
            {
            if (GetComponent<NavMeshAgent>()!=null)
                {
                GetComponent<NavMeshAgent>().enabled = true;
                }
            }
        }
    }
