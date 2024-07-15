using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Sirenix;
using UnityEngine.XR.Interaction.Toolkit;
using DogVR.Actions;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using DogVR;

public class TriggerHaptics : MonoBehaviour
{
    private XRInputModalityManager inputModalityManager;
    XRBaseController leftController;
    XRBaseController rightController;
    public WagOmeter onTriggerHaptic;
    public UIAudio TriggerHapticUI;

    private void Start()
        {
        inputModalityManager =GameManager.Instance.playerGameObjectSO.persistentObject.GetComponentInChildren<XRInputModalityManager>();
        if (leftController == null)
            {
            leftController = inputModalityManager.leftController.GetComponent<XRBaseController>();
            }
        if (rightController == null)
            {
            rightController = inputModalityManager.rightController.GetComponent<XRBaseController>();
            }

        onTriggerHaptic = GetComponent<WagOmeter>();
        if (onTriggerHaptic != null)
            {
            onTriggerHaptic.OnTriggerHaptic += SendHapticFeedback;
            }
        if (TriggerHapticUI != null)
            {
            TriggerHapticUI.OnTriggerHapticUI += SendHapticFeedback;
            }
        

        }

    private void OnDisable()
        {
        if (onTriggerHaptic != null) onTriggerHaptic.OnTriggerHaptic -= SendHapticFeedback;
        if (TriggerHapticUI != null) TriggerHapticUI.OnTriggerHapticUI -= SendHapticFeedback;
        }

    public void SendHapticFeedback(float Intensity, float Duration)
        {
        if (Intensity > 0f)
            {
            rightController.SendHapticImpulse(Intensity, Duration);
            leftController.SendHapticImpulse(Intensity, Duration);
            }
        }



    }
