using HutongGames.PlayMaker.Actions;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.XR.LegacyInputHelpers;
using UnityEngine;

namespace DogVR.Movement
    {
    public class DefaultCameraOffset : MonoBehaviour
        {
        [SerializeField]
        private CameraOffset cameraOffset;
        private Vector3 defaultPositionY;
        private Transform _camera;
        private void Awake()
            {
            _camera = GetComponent<Transform>();
            defaultPositionY = _camera.transform.localPosition;
            Debug.Log(defaultPositionY);
            }
            
        public void SetCameraHeightY()
            {
            _camera.transform.localPosition = new Vector3(0, cameraOffset.cameraYOffset, 0);
            //_camera.transform.localPosition = defaultPositionY;
            }

        public void SetCameraCrouchHeightY(float crouchOffsetY)
            {
            _camera.transform.localPosition = new Vector3(0, crouchOffsetY, 0);
            }
        }
    }
