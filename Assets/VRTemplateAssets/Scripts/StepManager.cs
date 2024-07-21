using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.XR.Interaction.Toolkit;
using DogVR;
using Unity.XR.CoreUtils;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;


namespace Unity.VRTemplate
    {
    /// <summary>
    /// Controls the steps in the in coaching card.
    /// </summary>


    public class StepManager : MonoBehaviour
        {
        [Serializable]
        class Step
            {
            [SerializeField]
            public GameObject stepObject;

            [SerializeField]
            public string buttonText;
            }
        private bool disableOnNextStep = false;

        [SerializeField]
        public TextMeshProUGUI m_StepButtonTextField;

        [SerializeField]
        List<Step> m_StepList = new List<Step>();

        [SerializeField]
        private GameObject gameObjectToDisable;

        [SerializeField]
        private EnableXRRigMovement enableXRRigMovement;

        private DisableXRRigMovement disableXRRigMovement;

        [SerializeField]
        private SetNewObjective SetNewObjective;

        [SerializeField]
        private PlayableDirector playableDirector;

        int m_CurrentStepIndex = 0;
        private XROrigin m_XROrigin;
        private GameObject m_RightController;
        private GameObject m_LeftController;


        private void Start()
            {
            if (TryGetComponent<DisableXRRigMovement>(out disableXRRigMovement))
                {
                disableXRRigMovement.DisableMovement();
                }
            m_XROrigin = GameManager.Instance.playerGameObjectSO.persistentObject.gameObject.GetComponentInChildren<XROrigin>();
            m_RightController = m_XROrigin.GetComponent<XRInputModalityManager>().rightController;
            m_LeftController = m_XROrigin.GetComponent<XRInputModalityManager>().leftController;
           
            }


        public void Next()
            {
            if (!disableOnNextStep)
                {
                m_StepList[m_CurrentStepIndex].stepObject.SetActive(false);
                m_CurrentStepIndex = (m_CurrentStepIndex + 1) % m_StepList.Count;
                m_StepList[m_CurrentStepIndex].stepObject.SetActive(true);
                m_StepButtonTextField.text = m_StepList[m_CurrentStepIndex].buttonText;
                if (m_StepButtonTextField.text == "Finish")
                    {
                    disableOnNextStep = true;
                    }
                }
            else
                {
                DisbaleGUI();
                }

            }

        public void DisbaleGUI()
            {
            if (gameObjectToDisable != null)
                {
                gameObjectToDisable.SetActive(false);

                EnableXrRigMovement();
                if (SetNewObjective != null)
                    {
                    GameManager.Instance.SetObjectivesManager.CurrentObjectiveIndex = 0;
                    }
                }
            }

        public void EnableXrRigMovement()
            {
            if (enableXRRigMovement != null)
                {
                m_RightController.GetComponent<ActionBasedControllerManager>().enabled = true;
                m_LeftController.GetComponent<ActionBasedControllerManager>().smoothMotionEnabled = true;
                Debug.Log("Enable Input Actions Right Controller");
                enableXRRigMovement.EnableMovement();
                EnablePlayableDirector();
                }
            }

        public void EnablePlayableDirector()
            {
            if (playableDirector != null)
                {
                playableDirector.enabled = true;
                }
            }
        }
    }
