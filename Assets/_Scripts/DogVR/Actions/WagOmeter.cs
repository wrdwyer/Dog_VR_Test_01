using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;
using FMODUnity;
using Sirenix;
using UnityEngine.XR.Interaction.Toolkit;

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
        private StudioEventEmitter growlandBark;
        [SerializeField]
        private StudioEventEmitter sadBark;
        [SerializeField]
        private StudioEventEmitter happyBark;
        [SerializeField]
        [Range(0f, 1f)]
        private float bbarkIntensity = 1f;
        [Range(0f, 10f)]
        [SerializeField]
        private float barkDuration = 1f;

        private void Awake()
            {
            CopperEmotionalState.currentState = CopperEmotionalState.AnimalState.Idle;
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

        public void SendHapticFeedback(float barkIntensity, float duration)
            {
            rightController.SendHapticImpulse(barkIntensity, duration);
            leftController.SendHapticImpulse(barkIntensity, duration);
            }


        public void Idle()
            {
            if (growlandBark.IsPlaying()) growlandBark.Stop();
            if (sadBark.IsPlaying()) sadBark.Stop();
            if (happyBark.IsPlaying()) happyBark.Stop();
            targetHappyAnimationParameter = 0f;
            targetSadAnimationParameter = 0f;
            SetParameters();
            Debug.Log("Idle");
            }

        [Button("Anxious")]
        public void Anxious()
            {
            targetHappyAnimationParameter = 1f;
            targetSadAnimationParameter = -1f;
            SetParameters();
            SendHapticFeedback(0.2f, 5f);
            sadBark.Play();
            Debug.Log("Anxious");
            }

        public void Happy()
            {
            targetHappyAnimationParameter = 0f;
            targetSadAnimationParameter = 1f;
            SetParameters();
            SendHapticFeedback(0.2f, 5f);
            happyBark.Play();
            Debug.Log("Happy");
            }

        public void Sad()
            {
            targetHappyAnimationParameter = 0f;
            targetSadAnimationParameter = -1f;
            SetParameters();
            Debug.Log("Sad");
            }

        public void Threatened()
            {
            targetHappyAnimationParameter = -1f;
            targetSadAnimationParameter = -1f;
            SetParameters();
            growlandBark.Play();
            SendHapticFeedback(0.2f, 5f);
            Debug.Log("Threatened");
            }

        }
    }
