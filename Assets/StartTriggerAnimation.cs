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
    private bool triggered = false;
    private void OnTriggerEnter(Collider other)
        {
        if (other.gameObject.CompareTag("Player") && !triggered)
            {
            Debug.Log("Start Animation");
            StartAnimation();
            triggered = true;
            }
        }

    public void StartAnimation()
        {
        playableDirector.Play();
        //startLeeAnimation.GetComponent<Animator>().enabled = true;
        }
    }
