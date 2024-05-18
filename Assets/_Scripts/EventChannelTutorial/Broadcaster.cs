using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.InputSystem;
using Sirenix.OdinInspector;

namespace Unity6Test
    {
    public class Broadcaster : MonoBehaviour
        {
        public VoidEventChannelSO eventChannel;
        public ScoreEventChannelSO scoreEventChannelSO;
        public ColourEventChannelSO colourEventChannel;
        public Color colourChange;
        public int damageToAdd = 10;

        private void Update()
            {
            if (eventChannel != null && Keyboard.current.spaceKey.wasPressedThisFrame)
                {
                eventChannel.RaiseEvent();
                Debug.Log("Space key was pressed");
                }
            if (colourEventChannel != null && Keyboard.current.leftArrowKey.wasPressedThisFrame)
                {
                colourEventChannel.RaiseEvent(colourChange);
                Debug.Log("Left arrow key was pressed");
                }
            if (scoreEventChannelSO != null && Keyboard.current.rightArrowKey.wasPressedThisFrame)
                {
                scoreEventChannelSO.RaiseEvent(damageToAdd);
                }

            }
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
        }

    }
    
