using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;

namespace DogVR.Actions
    {
    public class GuardDog : MonoBehaviour
        {
        private Animator animator;
        [SerializeField]
        private string attackName = "AttackReady";
        [SerializeField]
        private string sitName = "Sit";
        [SerializeField]
        private SplineAnimate splineAnimate = null;
        [SerializeField]
        private bool inRangeOfDog = false;

        private void Start()
            {
            animator = GetComponent<Animator>();
            animator.SetBool(sitName, true);
            animator.SetBool(attackName, false);
            }

        private void OnTriggerEnter(Collider other)
            {
            if (other.gameObject.CompareTag("Player"))
                {
                inRangeOfDog = true;
                animator.SetBool(sitName, false);
                animator.SetBool(attackName, true);
                }
            }

        private void OnTriggerExit(Collider other)
            {
            if ( other.gameObject.CompareTag("Player"))
            {
                inRangeOfDog = false;
                animator.SetBool(sitName, true);
                animator.SetBool(attackName, false);
                }
           
            }

        }
    }
