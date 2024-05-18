using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;

namespace DogVR.Actions
    {
    public class WagOmeter : MonoBehaviour
        {
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

        public void Idle()
            {
            targetHappyAnimationParameter = 0f;
            targetSadAnimationParameter = 0f;
            SetParameters();
            Debug.Log("Idle");
            }

        public void Anxious()
            {
            targetHappyAnimationParameter = 1f;
            targetSadAnimationParameter = -1f;
            SetParameters();
            Debug.Log("Anxious");
            }

        public void Happy()
            {
            targetHappyAnimationParameter = 0f;
            targetSadAnimationParameter = 1f;
            SetParameters();
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
            Debug.Log("Threatened");
            }

        }
    }
