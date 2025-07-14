using System;
using UnityEngine;

namespace RogueLikeCardSystem.Game.Utilities
{
    [System.Serializable]
    public class StatModifier<T> where T : struct, IComparable
    {
        public T Value;
        public StatModifierType Type;

        public StatModifier(T value, StatModifierType type)
        {
            Value = value;
            Type = type;
        }
    }
    
    public enum StatModifierType
    {
        Additive,
        Multiplicative
    }
}
