using UnityEngine;
using UnityEngine.Events;

///<summary>
///This class is usded to create a Events that have no arguments.
///</summary>
namespace Unity6Test
    {
    [CreateAssetMenu(menuName = "Events/VoidEventChannelSO")]
    public class VoidEventChannelSO : ScriptableObject
        {
        public UnityAction OnEventRaised;

        public void RaiseEvent()
            {
            OnEventRaised?.Invoke();
            }
        }
    }
