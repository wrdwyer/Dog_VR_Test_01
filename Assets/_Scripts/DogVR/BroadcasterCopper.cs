using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.InputSystem;
using Sirenix.OdinInspector;

using BlazeAISpace;
using FMODUnity;

namespace DogVR
    {
    [RequireComponent(typeof(SphereCollider))]
    public class BroadcasterCopper : MonoBehaviour
        {
        public AnimalStateChannelSO eventChannel;
        public CopperEmotionalState state;
        public Animator animator = null;
        public bool useAttackAnimation = false;
        public BlazeAI BlazeAI = null;
        public StudioEventEmitter attackSound;
        public bool Dog = true;
        public bool Hoodie = false;
        public bool girl = false;
        public bool Happy = false;
        public bool Anxious = false;
        public enum EncounteredAnimalState
            {
            Idle,
            Anxious,
            Happy,
            Sad,
            Threatened
            }
        public EncounteredAnimalState currentAnimalState = EncounteredAnimalState.Idle;
        [Multiline(2)]
        public string ToDoList = "To be added to anything which can trigger Coppers Emotional state.";

        private void Awake()
            {
            Debug.Log(state.currentState);
            animator = GetComponentInParent<Animator>();
            BlazeAI = GetComponentInParent<BlazeAI>();
            }

        private void OnTriggerEnter(Collider other)
            {
            Debug.Log("Trigger " + other.gameObject.name);
            if (other.CompareTag("Player"))
                {
                Debug.Log("Trigger Enter");

                switch (currentAnimalState)
                    {
                    case EncounteredAnimalState.Idle:
                        SetStatusToIdle();
                        break;
                    case EncounteredAnimalState.Anxious:
                        SetStatusToAnxious();
                        break;
                    case EncounteredAnimalState.Happy:
                        SetStatusToHappy();
                        break;
                    case EncounteredAnimalState.Sad:
                        SetStatusToSad();
                        break;
                    case EncounteredAnimalState.Threatened:
                        SetStatusToThreatened();
                        break;
                    default:
                        //SetStatusToIdle();
                        break;
                    }

                }
            }

        private void OnTriggerExit(Collider other)
            {
            if (other.CompareTag("Player"))
                SetStatusToIdle();
                {
                if (animator != null && BlazeAI != null && Dog)
                    {
                    Debug.Log("Trying to Idle");
                    animator.SetBool("AttackReady_b", false);
                    animator.SetBool("Sit_b", false);
                    animator.SetFloat("Movement_f", 0.5f);
                    BlazeAI.enabled = true;
                    if (attackSound != null) attackSound.Stop();
                    Debug.Log("Idle Ready");
                    }
                if (animator != null && BlazeAI != null && Dog && Happy)
                    {
                    Debug.Log("Trying to Walk");
                    animator.Play("Locomotion");
                    animator.SetFloat("Movement_f", 0.5f);
                   
                    BlazeAI.enabled = true;
                    if (attackSound != null) attackSound.Stop();
                    Debug.Log("Walk Ready");
                    }
                if (animator != null && BlazeAI != null && Hoodie)
                    {
                    Debug.Log("Trying to Idle");
                    animator.Play("Idle");
                    if (attackSound != null) attackSound.Stop();
                    Debug.Log("Idle Ready");
                    }
                if (animator != null && BlazeAI != null && girl)
                    {
                    Debug.Log("Trying to Idle");
                    animator.Play("Walking");
                    if (attackSound != null) attackSound.Stop();
                    BlazeAI.enabled = true;
                    Debug.Log("Idle Ready");
                    }
                if (animator == null && BlazeAI != null && Happy)
                    {
                    Debug.Log("Trying to Walk");
                    animator.Play("Walking");
                    if (attackSound != null) attackSound.Stop();
                    Debug.Log("Walk Ready");

                    }
                else
                    {
                    Debug.Log("Not Hoodie or Dog");
                    }
                }
            }

        [Button("Set Status to Happy")]
        private void SetStatusToHappy()
            {
            state.currentState = CopperEmotionalState.AnimalState.Happy;
            Debug.Log(state.currentState);
            TriggerEventChannel(state);
            if (animator != null && BlazeAI != null && Dog && Happy)
                {
                BlazeAI.enabled = false;
                Debug.Log("Trying to Be Happy");
                animator.SetFloat("Movement_f", 0.0f);
                animator.Play("TailWag");
                //attackSound.Play();
                Debug.Log("Happy Ready");
                }
            if (animator != null && BlazeAI != null && girl && Happy)
                {
                BlazeAI.enabled = false;
                Debug.Log("Trying to Be Happy");
                animator.Play("Waving");
                //attackSound.Play();
                Debug.Log("Happy Ready");
                }
            }

        [Button("Set Status to Sad")]
        private void SetStatusToSad()
            {
            state.currentState = CopperEmotionalState.AnimalState.Sad;
            Debug.Log(state.currentState);
            TriggerEventChannel(state);
            if (animator != null && BlazeAI != null && Happy)
                {
                BlazeAI.enabled = false;
                Debug.Log("Trying to Wave");
                animator.Play("Waving");
                attackSound.Play();
                Debug.Log("Attack Ready");
                }
            }

        [Button("Set Status to Anxious")]
        private void SetStatusToAnxious()
            {
            state.currentState = CopperEmotionalState.AnimalState.Anxious;
            Debug.Log(state.currentState);
            TriggerEventChannel(state);
            }

        [Button("Set Status to Idle")]
        private void SetStatusToIdle()
            {
            state.currentState = CopperEmotionalState.AnimalState.Idle;
            Debug.Log(state.currentState);
            TriggerEventChannel(state);
            }

        [Button("Set Status to Threatened")]
        private void SetStatusToThreatened()
            {
            state.currentState = CopperEmotionalState.AnimalState.Threatened;
            Debug.Log(state.currentState);
            TriggerEventChannel(state);
            if (animator != null && BlazeAI != null && Dog)
                {
                BlazeAI.enabled = false;
                Debug.Log("Trying to Attack");
                animator.SetFloat("Movement_f", 0.0f);
                animator.SetBool("AttackReady_b", true);
                attackSound.Play();
                Debug.Log("Attack Ready");
                }
            if (animator != null && Hoodie)
                {
                //BlazeAI.enabled = false;
                Debug.Log("Trying to Yell");
                animator.Play("Yelling");
                attackSound.Play();
                Debug.Log("Yell Ready");
                }

            }

        [Button("Trigger Copper Channel")]
        public void TriggerEventChannel(CopperEmotionalState _state)
            {
            eventChannel.RaiseEvent(_state);
            Debug.Log("Emotional State Sent" + _state.currentState);
            }
        /*
        [Button("Trigger Event Channel")]
        public void TriggerEventChannel()
            {
            eventChannel.RaiseEvent();
            Debug.Log("Space key was pressed");
            }

        [Button("Trigger Colour Event Channel")]
        public void TriggerColourEventChannel()
            {
            colourEventChannel.RaiseEvent(colourChange);
            Debug.Log("Left arrow key was pressed");
            }

        [Button("Trigger Score Event Channel")]
        public void TriggerScoreEventChannel()
            {
            scoreEventChannelSO.RaiseEvent(damageToAdd);
            }
        */
        }

    }

