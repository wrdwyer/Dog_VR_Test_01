using System;
using UnityEngine;

namespace Unity6Test
    {
    [CreateAssetMenu(menuName = "Variables/New Int Variable")]
    public class NewIntVariable : ScriptableObject
        {
        public int value;

        public static implicit operator NewIntVariable(int v)
            {
            throw new NotImplementedException();
            }
        }
    }
