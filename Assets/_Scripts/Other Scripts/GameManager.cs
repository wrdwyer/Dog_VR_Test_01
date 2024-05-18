using UnityEngine;

namespace Unity6Test
    {
    public class GameManager : MonoBehaviour
        {
        public static GameManager Instance;

        public int Score { get; private set; }
        public bool GameOver { get; private set; }

        private void Awake()
            {
            // Ensure there is only one GameManager
            if (Instance == null)
                {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                }
            else
                {
                Destroy(gameObject);
                }
            }

        private void Start()
            {
            GameOver = false;
            Score = 0;
            }

        public void AddScore(int scoreToAdd)
            {
            if (!GameOver)
                {
                Score += scoreToAdd;
                Debug.Log($"Score: {Score}");
                }
            }

        public void EndGame()
            {
            GameOver = true;
            Debug.Log("Game Over!");
            }

        // Call this method to reset the game
        public void ResetGame()
            {
            Score = 0;
            GameOver = false;
            Debug.Log("Game Reset!");
            }
        }
    }

