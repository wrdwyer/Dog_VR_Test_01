using DogVR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;

public class EnableBoneUI : MonoBehaviour
{
    [SerializeField] private GameObject gameObjectTextToEnable;
    [SerializeField] private float fadeDuration = 3f;
    [SerializeField] private TextMeshProUGUI textToRotate;
    private bool timerNotPlaying = true;
    


    void Update()
        {
        // Make sure the player transform is assigned
        if (textToRotate != null)
            {
            textToRotate.transform.rotation = Quaternion.LookRotation(textToRotate.transform.position - GameManager.Instance.playerGameObjectSO.persistentObject.GetComponentInChildren<XROrigin>().transform.position);
            }
        }




    private void OnTriggerEnter(Collider other)
        {
        if (gameObjectTextToEnable != null && other.tag == "Player" && timerNotPlaying)
            {
            timerNotPlaying = false;
            EnableTXT();
            StartCoroutine(FadeOutTimer());            
            }
        }

    //private void OnTriggerExit(Collider other)
    //    {
    //    if (gameObjectTextToEnable != null && other.tag == "Player")
    //        {
    //        DisabbleTXT();
    //        }
    //    }

    private IEnumerator FadeOutTimer()
        {
        yield return new WaitForSeconds(fadeDuration);
        timerNotPlaying = true;
        DisabbleTXT();
        }

    private void EnableTXT()
        {
        gameObjectTextToEnable.SetActive(true);
        }

    private void DisabbleTXT()
        {
        gameObjectTextToEnable.SetActive(false);
        }
    }
