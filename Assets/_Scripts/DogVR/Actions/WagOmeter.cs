using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;
using FMODUnity;
using Sirenix;
using UnityEngine.XR.Interaction.Toolkit;
using System;

namespace DogVR.Actions
    {
    public class WagOmeter : MonoBehaviour
        {
        [SerializeField]
        XRBaseController leftController;
        [SerializeField]
        XRBaseController rightController;
        [SerializeField]
        private string happyAnimationParameterName = "Happy";
        [SerializeField]
        private string sadAnimationParameterName = "Sad";
        private float targetHappyAnimationParameter = 0f;
        private float targetSadAnimationParameter = 0f;
        [SerializeField]
        private Animator animator;
        public CopperEmotionalState CopperEmotionalState;
        private CopperEmotionalState currentState;
        [SerializeField]
        private StudioEventEmitter idleBark;
        [SerializeField]
        private StudioEventEmitter growlandBark;
        [SerializeField]
        private StudioEventEmitter sadBark;
        [SerializeField]
        private StudioEventEmitter anxiousBark;
        [SerializeField]
        private StudioEventEmitter happyBark;

        [SerializeField]
        [Range(0f, 1f)]
        private float hapticIntensity = 1f;
        [Range(0f, 10f)]
        [SerializeField]
        private float hapticDuration = 1f;


        
        [Range(0f, 1f)]
        [SerializeField]
        private float AggressivehapticIntensity = 1f;
        [Range(0f, 10f)]
        [SerializeField]
        private float AggressivehapticDuration = 10f;

        public event Action <float,float>OnTriggerHaptic;

                private void Awake()
            {
            CopperEmotionalState.currentState = CopperEmotionalState.AnimalState.Happy;
            currentState = CopperEmotionalState;
            animator = GetComponent<Animator>();
            targetHappyAnimationParameter = 0f;
            targetSadAnimationParameter = 0f;
            SetParameters();
            }

       
        private void SetParameters()
            {
            DOTween.To(() => animator.GetFloat(happyAnimationParameterName), x => animator.SetFloat(happyAnimationParameterName, x), targetHappyAnimationParameter, 2f);
            DOTween.To(() => animator.GetFloat(sadAnimationParameterName), x => animator.SetFloat(sadAnimationParameterName, x), targetSadAnimationParameter, 2f);
            }
        public void SetEmotionalState(CopperEmotionalState state)
            {
            currentState = CopperEmotionalState;
            switch (CopperEmotionalState.currentState)
                {
                case CopperEmotionalState.AnimalState.Idle:
                    Idle();
                    //stateHasChanged = false;
                    break;
                case CopperEmotionalState.AnimalState.Anxious:
                    Anxious();
                    //stateHasChanged = false;
                    break;
                case CopperEmotionalState.AnimalState.Happy:
                    Happy();
                    //stateHasChanged = false;
                    break;
                case CopperEmotionalState.AnimalState.Sad:
                    Sad();
                    //stateHasChanged = false;
                    break;
                case CopperEmotionalState.AnimalState.Threatened:
                    Threatened();
                    //stateHasChanged = false;
                    break;
                default:
                    Idle();
                    //stateHasChanged = true;
                    break;
                }
            }

        public void TriggerHaptic(XRBaseController controller)
            {
            controller.SendHapticImpulse(hapticIntensity, hapticDuration);
            }

        public void SendHapticFeedback(float Intensity, float Duration)
            {
            if (Intensity > 0f)
                {
                rightController.SendHapticImpulse(Intensity, Duration);
                leftController.SendHapticImpulse(Intensity, Duration);
                }
            
           
            }


        public void Idle()
            {
            
            SendHapticFeedback(0.0f, 0f);
            StopAudio();
            targetHappyAnimationParameter = 0f;
            targetSadAnimationParameter = 0f;
            SetParameters();
            SendHapticFeedback(0.0f, 0f);      
            idleBark.Play();
            Debug.Log("Idle");
            }

        [Button("Anxious")]
        public void Anxious()
            {
            StopAudio();
            targetHappyAnimationParameter = 1f;
            targetSadAnimationParameter = -1f;
            SetParameters();
            SendHapticFeedback(0.2f, 5f);
            anxiousBark.Play();
            Debug.Log("Anxious");
            }

        public void Happy()
            {
            StopAudio();
            targetHappyAnimationParameter = 0f;
            targetSadAnimationParameter = 1f;
            SetParameters();
           
            SendHapticFeedback(0.2f, 1000f);
            happyBark.Play();
            Debug.Log("Happy");
            }

        public void Sad()
            {
            StopAudio();
            targetHappyAnimationParameter = 0f;
            targetSadAnimationParameter = -1f;
            SetParameters();
            sadBark.Play();
            SendHapticFeedback(0.2f, 5f);
            Debug.Log("Sad");
            }

        public void Threatened()
            {
            StopAudio();
            targetHappyAnimationParameter = -1f;
            targetSadAnimationParameter = -1f;
            SetParameters();
            growlandBark.Play();
            SendHapticFeedback(AggressivehapticIntensity, AggressivehapticDuration);
            Debug.Log("Threatened");
            }

        public void StopAudio()
            {
            if (idleBark.IsPlaying()) idleBark.Stop();
            if (growlandBark.IsPlaying()) growlandBark.Stop();
            if (sadBark.IsPlaying()) sadBark.Stop();
            if (happyBark.IsPlaying()) happyBark.Stop();
            if (anxiousBark.IsPlaying()) anxiousBark.Stop();
            }

        [Button("Test Haptics")]
        public void TestHaptics(float hapticIntensity, float hapticDuration)
            {
           OnTriggerHaptic?.Invoke(hapticIntensity, hapticDuration);
            }
        }
    }
