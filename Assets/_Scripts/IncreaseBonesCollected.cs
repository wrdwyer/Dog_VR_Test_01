using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DogVR;
public class IncreaseBonesCollected : MonoBehaviour
{
    public void IncreaseBonesCollecteSO()
        {
        GameManager.Instance.BonesCollectedSO.bonesCollected += 1;
        GameManager.Instance.playerGameObjectSO.persistentObject.GetComponentInChildren<UpdateBoneScoreUI>().UpdateBoneSoreUIText();
        }
}
