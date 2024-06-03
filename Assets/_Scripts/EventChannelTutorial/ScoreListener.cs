
using UnityEngine;

using TMPro;



namespace Unity6Test
    {
    public class ScoreListener : MonoBehaviour
        {
        public ScoreEventChannelSO scoreEventChannel;
        public NewIntVariable playerScore;
        public TextMeshProUGUI scoreText;

        public void Awake()
            {
            playerScore.value = 0;
            scoreText.text = playerScore.value.ToString();
            }

        public void OnEnable()
            {
            scoreEventChannel.OnEventRaised += IncreaseScore;
            }

        public void OnDisable()
            {
            scoreEventChannel.OnEventRaised -= IncreaseScore;
            }

        private void IncreaseScore(int scoreToAdd)
            {
            playerScore.value += scoreToAdd;
            scoreText.text = playerScore.value.ToString();
            }

        }
    }

