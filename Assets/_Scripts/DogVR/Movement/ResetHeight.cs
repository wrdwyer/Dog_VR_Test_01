using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix;
using Unity.XR.CoreUtils;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
using Sirenix.OdinInspector;

namespace DogVR.Movement
    {
    public class ResetHeight : MonoBehaviour
        {
        [Tooltip("Crouch Input Action")]
        [Required("Crouch Input Action")]
        [SerializeField]
        private InputActionReference standCrouchInputAction;
        [Tooltip("XR Origin (XR Rig) Camera Offset")]
        [Required("XR Origin (XR Rig) Camera Offset")]
        [SerializeField]
        private DefaultCameraOffset cameraOffset;
        [Tooltip("Crouch Offset")]
        [Required("Crouch Offset")]
        [SerializeField]
        private float crouchOffsetY = 0.3f;

        private void Update()
            {
            bool jumpVal = standCrouchInputAction.action.IsPressed();
            if (jumpVal)
                {                             
                Crouch();
                }
            if (jumpVal = standCrouchInputAction.action.WasReleasedThisFrame())
                {
                Stand();
                }
            }
        private void Crouch()
            {
            cameraOffset.SetCameraCrouchHeightY(crouchOffsetY);
            Debug.Log("Crouching");
            }

        private void Stand()
            {
            cameraOffset.SetCameraHeightY();
            Debug.Log("Standing");
            }
        }
    }
