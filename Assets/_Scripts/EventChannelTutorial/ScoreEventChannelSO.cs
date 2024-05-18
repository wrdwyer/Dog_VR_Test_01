using UnityEngine;
using UnityEngine.Events;

namespace Unity6Test
    {
    [CreateAssetMenu(menuName = "Events/Increase Score Event Channel")]
    public class ScoreEventChannelSO : ScriptableObject
        {                      
            public UnityAction<int>OnEventRaised;
        public void RaiseEvent(int scoreAmountToAdd)
            {
            OnEventRaised?.Invoke(scoreAmountToAdd);
            }
        }
    }

