using UnityEngine;
using UnityEngine.Events;

///<summary>
///This class is usded to create a Events that pass colour.
///</summary>
namespace Unity6Test
{
    [CreateAssetMenu(menuName = "Events/Colour Event Channel")]
    public class ColourEventChannelSO : ScriptableObject
        {
        public UnityAction<Color> OnEventRaised;

        public void RaiseEvent(Color color)
            {
            OnEventRaised?.Invoke(color);
            }
        }
    }
