using DogVR;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DogVR
    {
    public class SetObjectiveInfo : MonoBehaviour
        {
        [SerializeField]
        private CurrentObjectiveSO CurrentObjectiveSO;
        [SerializeField]
        private Toggle ObjectiveStateToggle;
        [SerializeField]
        private TextMeshProUGUI currentObjectiveName;
        [SerializeField]
        private TextMeshProUGUI currentBonesCollected;
        [SerializeField]
        private TextMeshProUGUI currentBonesCollectedUI;

        private void OnEnable()
            {
            currentObjectiveName.text = CurrentObjectiveSO.ObjectiveName;
            ObjectiveStateToggle.isOn = CurrentObjectiveSO.ObjectiveComplete;
            currentBonesCollected.text = GameManager.Instance.BonesCollectedSO.bonesCollected.ToString();
            currentBonesCollectedUI.text = GameManager.Instance.BonesCollectedSO.bonesCollected.ToString();
            }

        }
    }
