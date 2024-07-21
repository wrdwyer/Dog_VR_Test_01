using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class AnimationAudioEvent : MonoBehaviour
{
    public StudioEventEmitter emitter;
  public void Footstep()
        {
        emitter.Play();
        Debug.Log("Crunch");
        }
}
