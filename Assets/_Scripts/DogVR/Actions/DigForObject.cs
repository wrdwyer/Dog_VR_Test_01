using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VRTemplate;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace DogVR.Actions
    {
    public class DigForObject : MonoBehaviour
        {

        XRBaseController leftController;

        XRBaseController rightController;
        public WagOmeter wagOmeter;
        [SerializeField] float dirtReductionAmount = -0.035f;
        [SerializeField] int digsToRevealObject = 5;
        [SerializeField] Transform digMound;
        [SerializeField] GameObject objectToReveal;
        [SerializeField] ParticleSystem dirtParticleSystem;
        [SerializeField] int nextObjective;
        [SerializeField] StudioEventEmitter StudioEventEmitter;
        [SerializeField] GameObject nextObjectiveGO;
        [SerializeField] BoxCollider boxCollider;
        [SerializeField]
        [Range(0f, 1f)]
        private float hapticIntensity = 1f;
        [Range(0f, 10f)]
        [SerializeField]
        private float hapticDuration = 1f;
        private int _digCount = 0;
        private float _moundPosition;
        private float _objectPosition;
        private float _amountToMove;


        void Start()
            {
            //Debug.Log(GameManager.Instance.playerGameObjectSO.persistentObject.GetComponentInChildren<WagOmeter>());
            //wagOmeter = GameManager.Instance.playerGameObjectSO.persistentObject.GetComponentInChildren<WagOmeter>();
            leftController = wagOmeter.leftController;
            rightController = wagOmeter.rightController;
            _amountToMove = dirtReductionAmount / digsToRevealObject;
            _moundPosition = digMound.localPosition.y;
            _objectPosition = objectToReveal.transform.localPosition.y;
            }

        public void SendHapticFeedback(float Intensity, float Duration)
            {
            if (Intensity > 0f)
                {
                rightController.SendHapticImpulse(Intensity, Duration);
                leftController.SendHapticImpulse(Intensity, Duration);
                }


            }

        private void OnTriggerEnter(Collider other)
            {
            if (other.CompareTag("PhysicsHand"))
                {
                if (StudioEventEmitter != null)
                    {
                    StudioEventEmitter.Play();
                    SendHapticFeedback(hapticIntensity, hapticDuration);
                    }
                Dig();
                }
            }

        void Dig()
            {
            if (_digCount < digsToRevealObject)
                {
                RevealObject();
                }
            else
                {
                EnableXRcomponents();
                GetComponent<SetNewObjective>().SetNewObjectiveGO(nextObjective);
                nextObjectiveGO.SetActive(true);
                }

            }

        // RevealObject method to reveal an object by playing a particle system, adjusting positions, and updating object visibility.
        private void RevealObject()
            {
            dirtParticleSystem.Play();
            _moundPosition += _amountToMove;
            digMound.localPosition = new Vector3(0, _moundPosition, 0);
            _objectPosition -= _amountToMove;
            objectToReveal.transform.localPosition = new Vector3(0, _objectPosition, 0);
            _digCount++;
            }

        // Enable XR components on the object to reveal.
        private void EnableXRcomponents()
            {
            objectToReveal.SetLayerRecursively(11);
            objectToReveal.GetComponent<XRGrabInteractable>().enabled = true;
            objectToReveal.GetComponent<RayAttachModifier>().enabled = true;
            objectToReveal.GetComponent<Rigidbody>().isKinematic = false;
            boxCollider.enabled = false;
            this.enabled = false;
            StudioEventEmitter.Stop();
            }

        }
    }
