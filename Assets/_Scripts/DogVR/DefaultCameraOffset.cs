using HutongGames.PlayMaker.Actions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DogVR.Movement
    {
    public class DefaultCameraOffset : MonoBehaviour
        {
        
        private Vector3 defaultPositionY;
        private Transform camera;
        private void Awake()
            {
            camera = GetComponent<Transform>();
            defaultPositionY = camera.transform.localPosition;
            Debug.Log(defaultPositionY);
            }
            
        public void SetCameraHeightY()
            { 
            camera.transform.localPosition = defaultPositionY;
            }

        public void SetCameraCrouchHeightY(float crouchOffsetY)
            {
            camera.transform.localPosition = new Vector3(0, crouchOffsetY, 0);
            }
        }
    }
