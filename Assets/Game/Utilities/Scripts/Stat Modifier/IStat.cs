using System;
using RogueLikeCardSystem.Game.Utilities;
using UnityEngine;

namespace RogueLikeCardSystem.Game.Utilities
{
    public interface IStat<T> where T : struct, IComparable
    {
        T BaseValue { get; }
        T Value { get; }
        void SetBaseValue(T value);
        void AddModifier(StatModifier<T> modifier);
        void RemoveModifier(StatModifier<T> modifier);
        void ClearModifiers();
    }
}
