using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using FMODUnity;
using DogVR.Actions;
using System;
using Sirenix.OdinInspector;

public class UIAudio : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
    public StudioEventEmitter clickAudioName;
    public StudioEventEmitter hoverEnterAudioName;
    public StudioEventEmitter hoverExitAudioName;
    public event Action<float, float> OnTriggerHapticUI;
    public float UIhapticIntensity = 1f;
    public float UIhapticDuration = 5f;

    public void OnPointerClick(PointerEventData eventData)
        {
        if (clickAudioName != null)
            {
            clickAudioName.Play();
            TestUIHaptics(UIhapticIntensity, UIhapticDuration);
            //AudioManager.instance.Play(clickAudioName);
            }
        }

    public void OnPointerEnter(PointerEventData eventData)
        {
        if (hoverEnterAudioName != null)
            {
            hoverEnterAudioName.Play();
            TestUIHaptics(UIhapticIntensity, UIhapticDuration);
            // AudioManager.instance.Play(hoverEnterAudioName);
            }
        }

    public void OnPointerExit(PointerEventData eventData)
        {
        if (hoverExitAudioName != null)
            {

            //hoverExitAudioName.Play();
            // AudioManager.instance.Play(hoverExitAudioName);
            }
        }

    [Button("Test Haptics")]
    public void TestUIHaptics(float UIhapticIntensity, float UIhapticDuration)
        {
        OnTriggerHapticUI?.Invoke(UIhapticIntensity, UIhapticDuration);
        }
    }
    
