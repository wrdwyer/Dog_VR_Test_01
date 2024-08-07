using DogVR.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.XR.LegacyInputHelpers;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;



namespace DogVR
    {
    public class SetOptionDefaults : MonoBehaviour
        {
        [SerializeField]
        private VROptionsSO options = null;
        [SerializeField]
        private Toggle colourSpectrum = null;
        [SerializeField]
        private Toggle vignette = null;
        [SerializeField]
        private GameObject vignetteObject = null;
        [SerializeField]
        private Toggle snapTunrn = null;
        private Volume volume;
        [SerializeField]
        private ResetHeight resetHeight;

        [SerializeField]
        private CameraOffset cameraOffset;
        private ColorLookup colorLookup;
        private void OnEnable()
            {
            if (volume == null)
                {
                volume = GameManager.Instance.VolumePostProcessingVolume;
                }
            if (colourSpectrum != null)
                {
                colourSpectrum.isOn = options.enableDogVision;
                SetDogVision();
                }
            if (vignette != null)
                {
                vignette.isOn = options.enableVignette;
                }
            if (snapTunrn != null)
                {
                snapTunrn.isOn = options.snapTurn;
                }
            }



        public void SetDogVision()
            {
            if (colourSpectrum != null)
                {
                VolumeProfile profile = volume.sharedProfile;
                if (!profile.TryGet(out colorLookup))
                    {
                    SetDogVision();
                    }
                }
            options.enableDogVision = colourSpectrum.isOn;
            if (colourSpectrum.isOn)
                {
                colorLookup.active = true;
                }
            else
                {
                colorLookup.active = false;
                }
            }



        public void SetVignette()
            {
            if (vignette != null)
                {
                options.enableVignette = vignette.isOn;
                if (options.enableVignette)
                    {
                    vignetteObject.SetActive(true);

                    }
                else
                    {
                    vignetteObject.SetActive(false);
                    }
                }
            }
        public void SetSnapTunrn()
            {
            if (snapTunrn != null)
                {
                options.snapTurn = snapTunrn.isOn;
                }
            }
        public void ResetCamerHeight()
            {
            cameraOffset.enabled = false;
            cameraOffset.enabled = true;
            //resetHeight.Stand();
            Debug.Log("Resetting Camera Height");
            }

        public void QuitToMenu()
            {
#if UNITY_STANDALONE
        Application.Quit();
#endif

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Debug.Log("Quitting to Menu");
            }
        }
    }
