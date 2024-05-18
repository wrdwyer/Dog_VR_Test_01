using DogVR.Actions;
using System;
using System.Collections;
using UnityEngine;


namespace DogVR
    {
    [RequireComponent(typeof(WagOmeter))]
    public class ListenerCopper : MonoBehaviour
        {
        public AnimalStateChannelSO animalStateChannel;   
        public CopperEmotionalState state;
        private WagOmeter wagOmeter;

        public void Awake()
            {
            wagOmeter = GetComponent<WagOmeter>();
            }

        public void OnEnable()
            {          
            animalStateChannel.OnEventRaised += UpdateEmotionalState;
            }

        public void OnDisable()
            {
            animalStateChannel.OnEventRaised -= UpdateEmotionalState;
            }

        private void UpdateEmotionalState(CopperEmotionalState arg0)
            {
            state = arg0;
            wagOmeter.SetEmotionalState(state);
            }
        }
    }
