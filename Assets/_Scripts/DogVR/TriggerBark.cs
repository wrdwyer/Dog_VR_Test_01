using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Oculus.Haptics;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using FMODUnity;
//using Interhaptics.Core;
//using Interhaptics;

namespace DogVR.Actions
    {
    public class TriggerBark : MonoBehaviour
        {


        //public HapticClip hapticClip;
        public InputActionReference barkInput = null;
        public InputActionReference hapticInput = null;
        public XRBaseController controller;
        public StudioEventEmitter audioEmitter;
        //public HapticMaterial myHapticMaterial; // Assign this in the Unity Inspector

        private bool barking = false;


        [Range(0, 2.5f)]
        public float duration;
        [Range(0, 1)]
        public float amplitude;
        // [Range(0, 1)]
        // public float frequency;


        //public GrabInteractable grabInteractable;
        //private HapticClipPlayer clipPlayer;

        // Start is called before the first frame update
        private void Awake()
            {
            /*if (HAR.Init())
                {
                Debug.Log("Haptic initialized");
                }*/
            controller = GetComponent<XRBaseController>();

            }

        void Start()
            {
            //clipPlayer = new HapticClipPlayer(hapticClip);

            //grabInteractable.WhenSelectingInteractorAdded.Action += WhenSelectingInteractorAdded_Action;
            }

     

        void Update()

            {
            bool barkPressed = barkInput.action.IsPressed();
            bool barkReleased = barkInput.action.WasReleasedThisFrame();



            if (barkPressed && !barking && audioEmitter.IsPlaying() == false)
                {
                Bark();
                barking = true;
                }
            if (barkReleased)
                {
                barking = false;

                }
            }


        public void Bark()
            {
            EventManager.Instance.Bark();
            Debug.Log("Bark!");
            PlayMyHapticEffect();
            audioEmitter.Play();
           
            //controller.SendHapticImpulse(amplitude, duration);

            //clipPlayer.Play();
            }

        void PlayMyHapticEffect()
            {
            /*if (myHapticMaterial != null)
                {
                HAR.PlayHapticEffect(myHapticMaterial);
                Debug.Log("Haptic effect played successfully!");
                }
            else
                {
                Debug.LogError("Haptic material is not assigned!");
                }
            }
        private void WhenSelectingInteractorAdded_Action(GrabInteractor obj)
            {
            ControllerRef controllerRef = obj.GetComponent<ControllerRef>();
            if (controllerRef)
                {
                if (controllerRef.Handedness == Handedness.Right)
                    TriggerHaptics(OVRInput.Controller.RTouch);
                else
                    TriggerHaptics(OVRInput.Controller.LTouch);
                }
            }/*

        /*public void TriggerHaptics(OVRInput.Controller controller)
            {
            if (hapticClip)
                {
                if (controller == OVRInput.Controller.RTouch)
                    {
                    clipPlayer.Play(Oculus.Haptics.Controller.Right);
                    }
                else if (controller == OVRInput.Controller.LTouch)
                    {
                    clipPlayer.Play(Oculus.Haptics.Controller.Left);
                    }
                }
            else
                { }
            // StartCoroutine(TriggerHapticsRoutine(controller));
            }/*

        /*public IEnumerator TriggerHapticsRoutine(OVRInput.Controller controller)
            {
            OVRInput.SetControllerVibration(frequency, amplitude, controller);
            yield return new WaitForSeconds(duration);
            OVRInput.SetControllerVibration(0, 0, controller);
            }
        */
            }
        }
    }
