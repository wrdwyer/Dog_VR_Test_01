using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.XR.Interaction.Toolkit;

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
        private PlayableDirector playableDirector;

        int m_CurrentStepIndex = 0;

        private void Start()
            {
            GetComponent<DisableXRRigMovement>().DisableMovement();
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
                }
            }

        public void EnableXrRigMovement()
            {
            if (enableXRRigMovement != null)
                {
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
