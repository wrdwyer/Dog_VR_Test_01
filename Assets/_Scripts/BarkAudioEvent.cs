using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class BarkAudioEvent : MonoBehaviour
{
    public StudioEventEmitter emitter;
  public void Bark()
        {
        emitter.Play();
        Debug.Log("Bark");
        }
}
