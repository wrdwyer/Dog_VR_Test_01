using System;
using System.Collections;
using UnityEngine;


namespace Unity6Test
    {
    public class Listener : MonoBehaviour
        {
        public VoidEventChannelSO eventChannel;
        public ColourEventChannelSO ColourEventChannel;
        public GameObject gameObjectDisable;
        public GameObject gameObjectChangeColour;
        public MeshRenderer meshRenderer;
        

        public void Awake()
            {
            meshRenderer = gameObjectChangeColour.GetComponent<MeshRenderer>();
            }

        public void OnEnable()
            {
            eventChannel.OnEventRaised += Jump;
            ColourEventChannel.OnEventRaised += ChangeColour;
            }

        public void OnDisable()
            {
            eventChannel.OnEventRaised -= Jump;
            ColourEventChannel.OnEventRaised -= ChangeColour;
            }

        void Jump()
            {
            gameObjectDisable.SetActive(false);
            Debug.Log("Jump!");
            }

        public void ChangeColour(Color colourChange)
            {
            meshRenderer.material.color = colourChange;
            Debug.Log("Color changed to: " + colourChange);
            }
        }
    }
