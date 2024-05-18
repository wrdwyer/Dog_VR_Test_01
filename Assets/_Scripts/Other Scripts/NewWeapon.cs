using UnityEngine;
using System;


namespace Unity6Test
{
    public class NewWeapon : ScriptableObject
    {
        [CreateAssetMenu(menuName = "Variables/New Weapon_with_damage")]
        
        public class NewIntVariable : ScriptableObject
            {
            public Action<int> OnEventRaised;
            public void RaiseEvent(int damage)
                {
                OnEventRaised?.Invoke(damage);
                }
            }
        }
}
