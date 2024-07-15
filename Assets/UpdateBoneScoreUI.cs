using DogVR;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateBoneScoreUI : MonoBehaviour
    {
    [SerializeField]
    private TextMeshProUGUI BoneScoreUI;
    public void UpdateBoneSoreUIText()
        {
        BoneScoreUI.text = GameManager.Instance.BonesCollectedSO.bonesCollected.ToString();
        }
    }
