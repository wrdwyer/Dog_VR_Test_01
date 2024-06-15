using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DogVR.Objects
    {
    public class SeeSaw : MonoBehaviour
        {
        [SerializeField]
        private StudioEventEmitter SoundEffect;
       // [SerializeField]
        //private StudioEventEmitter seeSawFinish;
        [SerializeField]
        private BoxCollider BoxColliderToEndable;
        //[SerializeField]
        //private BoxCollider BoxColliderFinish;
       //[SerializeField]
       //private bool startEnabled = true;
        [SerializeField]
        private Animator animator;
        [SerializeField]
        private string AnimationName;

        // Start is called before the first frame update

     
        private void OnTriggerEnter(Collider other)
            {
            if (other.gameObject.CompareTag("Player"))
                {
                Debug.Log("SeeSawStart");
                animator.Play(AnimationName);
                SoundEffect.Play();
                BoxColliderToEndable.enabled = true;
                GetComponent<BoxCollider>().enabled = false;
                }           
            }
        }
    }
