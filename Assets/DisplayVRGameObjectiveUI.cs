using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DisplayVRGameObjectiveUI : MonoBehaviour
{
    [SerializeField]
    private InputActionReference menuButton;
    [SerializeField]
    private GameObject VRUIMenu;

    private void Update()
        {
        if (menuButton.action.WasPressedThisFrame())
            if (menuButton != null)
                {
                if (menuButton.action.IsPressed())
                    {
                    EnableDisableVRUI();
                    }
                }
        }
    [Button("EnableDisableVRUI")]
    private void EnableDisableVRUI()
        {
        VRUIMenu.SetActive(!VRUIMenu.activeSelf);
        }
    }
