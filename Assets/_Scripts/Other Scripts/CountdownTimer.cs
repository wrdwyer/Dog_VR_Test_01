using UnityEngine;
using UnityEngine;
using System.Collections;
using System;

namespace Unity6Test
{
    public class CountdownTimer : MonoBehaviour
        {
        private int duration = 5; // Default countdown duration
        public GameEvent gameEvent;
        public GameEvent gameEvent2;
        public IntEventChannelSO IntEventChannelSO;
        public int scoreToAdd = 5; 
        public void StartCountdown(int seconds)
            {
            //StartCoroutine(Countdown(seconds));
            gameEvent.TriggerEvent();
            gameEvent2.TriggerEvent();
         
            }

        private IEnumerator Countdown(int seconds)
            {
            while (seconds > 0)
                {
                Debug.Log("Countdown: " + seconds + " seconds remaining");
                yield return new WaitForSeconds(1);
                seconds--;
                }

            Debug.Log("Countdown finished!");
            }
        }
    }
