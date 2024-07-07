using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.InputSystem;
using Sirenix.OdinInspector;

namespace DogVR
    {
    [RequireComponent(typeof(SphereCollider))]
    public class BroadcasterCopper : MonoBehaviour
        {
        public AnimalStateChannelSO eventChannel;
        public CopperEmotionalState state;
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
                        SetStatusToIdle();

                        break;
                    }

                }
            }

        private void OnTriggerExit(Collider other)
            {
            SetStatusToIdle();
            }

        [Button("Set Status to Happy")]
        private void SetStatusToHappy()
            {
            state.currentState = CopperEmotionalState.AnimalState.Happy;
            Debug.Log(state.currentState);
            TriggerEventChannel(state);
            }

        [Button("Set Status to Sad")]
        private void SetStatusToSad()
            {
            state.currentState = CopperEmotionalState.AnimalState.Sad;
            Debug.Log(state.currentState);
            TriggerEventChannel(state);
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

