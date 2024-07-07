using DogVR.Actions;
using DogVR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableBark : MonoBehaviour
    {
    // Start is called before the first frame update
    public void EnableBarking()
        {
        GameManager.Instance.playerGameObjectSO.persistentObject.GetComponentsInChildren<TriggerBark>()[0].enabled = true;
        }

    }
