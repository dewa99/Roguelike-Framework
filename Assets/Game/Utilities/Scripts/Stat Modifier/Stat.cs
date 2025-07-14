using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RogueLikeCardSystem.Game.Utilities
{
    
    [Serializable]
    public class Stat<T>:  IStat<T> where T : struct, IComparable
    {
        [SerializeField] private T baseValue;
        private List<StatModifier<T>> modifiers = new();
        
        public Stat() { }

        public Stat(T baseValue)
        {
            this.baseValue = baseValue;
        }

        public Stat(T baseValue, IEnumerable<StatModifier<T>> modifiers)
        {
            this.baseValue = baseValue;
            this.modifiers = new List<StatModifier<T>>(modifiers);
        }

        public T BaseValue => baseValue;
        
        public virtual T Value
        {
            get
            {
                dynamic value = baseValue;

                foreach (var mod in modifiers.Where(m => m.Type == StatModifierType.Additive))
                    value += (dynamic)mod.Value;

                foreach (var mod in modifiers.Where(m => m.Type == StatModifierType.Multiplicative))
                    value *= (dynamic)mod.Value;

                return (T)value;
            }
        }

        public void SetBaseValue(T value) => baseValue = value;
        public void AddModifier(StatModifier<T> modifier) => modifiers.Add(modifier);
        public void RemoveModifier(StatModifier<T> modifier) => modifiers.Remove(modifier);
        public void ClearModifiers() => modifiers.Clear();
        
        public Stat<T> Clone()
        {
            return new Stat<T>(baseValue, modifiers.Select(m => new StatModifier<T>(m.Value, m.Type)));
        }
    }
}
