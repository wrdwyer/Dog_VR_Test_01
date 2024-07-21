using DogVR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class StartTriggerAnimation : MonoBehaviour
    {
    [SerializeField]
    private GameObject startLeeAnimation;
    [SerializeField]
    private PlayableDirector playableDirector;
    [SerializeField]
    private GameObject telportationAreaGO;
    private bool triggered = false;
    private void OnTriggerEnter(Collider other)
        {
        if (other.gameObject.CompareTag("Player") && !triggered)
            {
            telportationAreaGO.SetActive(true);
            Debug.Log("Start Animation");
            StartAnimation();
            triggered = true;
            //GameManager.Instance.playerGameObjectSO.persistentObject.GetComponentInChildren<SetCameraYOffset>().enabled = true;
            //GameManager.Instance.playerGameObjectSO.persistentObject.GetComponentInChildren<SetCameraYOffset>().enabled = false;
            }
        }

    public void StartAnimation()
        {
        playableDirector.Play();
        //startLeeAnimation.GetComponent<Animator>().enabled = true;
        }
    }
