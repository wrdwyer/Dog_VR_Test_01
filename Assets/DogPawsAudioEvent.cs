using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class DogPawsAudioEvent : MonoBehaviour
    {
    public StudioEventEmitter emitter;
    public void DogPaw()
        {
        emitter.Play();
        
        }
    }
