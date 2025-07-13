using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RoguelikeCardSystem.Game.Utilities
{
    /// <summary>
    /// KeyValuePair but is a class and serializable
    /// </summary>
    /// <typeparam name="TData">The type of the associated data.</typeparam>
    /// <typeparam name="TValue">The type of the associated value.</typeparam>
    [Serializable]
    public class ObjectPair<TData, TValue>
    {
        [SerializeField] protected TData data; //TODO: Rename this to 'key'
        [SerializeField] protected TValue value;

        /// <summary>
        /// The first item of the object pair
        /// </summary>
        public TData Data => data;
        /// <summary>
        /// The second item of the object pair
        /// </summary>
        public TValue Value
        {
            get => value;
            set => this.value = value;
        }

        public ObjectPair(TData data, TValue value)
        {
            this.data = data;
            this.value = value;
        }

        public void Deconstruct(out TData data, out TValue value)
        {
            data = this.data;
            value = this.value;
        }

        public static implicit operator KeyValuePair<TData, TValue>(ObjectPair<TData, TValue> objectPair)
        {
            return new KeyValuePair<TData, TValue>(objectPair.data, objectPair.value);
        }

        public override string ToString()
        {
            return $"[{data}, {value}] ({typeof(TData).Name}, {typeof(TValue).Name})";
        }
    }

    /// <summary>
    /// Collection of <see cref="ObjectPair{TData,TValue}"/>
    /// </summary>
    /// <typeparam name="TData">The type of first item in the pair.</typeparam>
    /// <typeparam name="TValue">The type of second item in the pair.</typeparam>
    [Serializable]
    public class ObjectPairCollection<TData, TValue> : ClassWithLogger, IEnumerable<ObjectPair<TData, TValue>>
    {
        [SerializeField] protected List<ObjectPair<TData, TValue>> items = new();

        public ObjectPairCollection()
        {
        }

        public ObjectPairCollection(IEnumerable<ObjectPair<TData, TValue>> pairs)
        {
            items = new(pairs);
        }

        public int Count => items.Count;

        public int FindIndex(Predicate<ObjectPair<TData, TValue>> match)
        {
            return items.FindIndex(match);
        }

        /// <summary>
        /// Gets the object pair in the specified <paramref name="index"/>
        /// </summary>
        public virtual ObjectPair<TData, TValue> this[int index] => items[index];

        /// <summary>
        /// Gets the <see cref="TValue"/> associated with the specified <paramref name="data"/>.
        /// </summary>
        /// <remarks>
        /// Gets the first pair with <paramref name="data"/> found, and returns it's <see cref="TValue"/>. 
        /// <para>Will not raise an error. When no <paramref name="data"/> found, simply returns null</para>
        /// </remarks>
        /// <param name="data">The <see cref="TData"/> to search for in the object pairs.</param>
        public virtual TValue this[TData data]
        {
            get
            {
                var objectPair = items.FirstOrDefault(p => p.Data.Equals(data));
                return objectPair != null ? objectPair.Value : default;
            }
        }

        public virtual IEnumerator<ObjectPair<TData, TValue>> GetEnumerator() => items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    /// <summary>
    /// List of <see cref="ObjectPair{TData,TValue}"/>. 
    /// </summary>
    /// <typeparam name="TData">The type of first item in the pair.</typeparam>
    /// <typeparam name="TValue">The type of second item in the pair.</typeparam>
    [Serializable]
    public class ObjectPairList<TData, TValue> : ObjectPairCollection<TData, TValue>
    {
        public ObjectPairList()
        {
        }

        public ObjectPairList(IEnumerable<ObjectPair<TData, TValue>> pairs) : base(pairs)
        {
        }

        public virtual void Add(TData data, TValue value) => items.Add(new(data, value));

        public virtual void Add(ObjectPair<TData, TValue> item) => items.Add(item);

        public virtual bool Remove(TData item)
        {
            var pairToRemove = items.FirstOrDefault(pair => Equals(pair.Data, item));

            if (pairToRemove != null)
            {
                items.Remove(pairToRemove);
                return true;
            }

            return false;
        }

        public int RemoveAll(Predicate<ObjectPair<TData, TValue>> predicate) => items.RemoveAll(predicate);

        public void Clear() => items.Clear();
    }

    /// <summary>
    /// Serializable dictionary of <see cref="ObjectPair{TData,TValue}"/>
    /// </summary>
    /// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
    /// <remarks>
    /// If there's a duplicate items set from the inspector, it will only warn you.
    /// Only unique pairs will get enumerated. 
    /// </remarks>
    [Serializable]
    public class ObjectPairDictionary<TKey, TValue> : ObjectPairList<TKey, TValue>, ISerializationCallbackReceiver
    {
        public ObjectPairDictionary()
        {
        }

        public ObjectPairDictionary(IEnumerable<ObjectPair<TKey, TValue>> pairs) : base(pairs)
        {
        }

        private IEnumerable<ObjectPair<TKey, TValue>> DistinctItems => items.DistinctBy(pair => pair.Data);

        public void OnBeforeSerialize()
        {
            // No need to do anything before serialization
        }

        public void OnAfterDeserialize()
        {
#if UNITY_EDITOR
            // Check for duplicates and log warnings
            var keys = new HashSet<TKey>();
            foreach (var pair in items)
            {
                if (keys.Contains(pair.Data))
                {
                    UnityEditor.EditorApplication.delayCall += () =>
                    Debug.LogWarning($"Duplicate key found. Duplicate entry will be ignored when enumerating. " +
                                     $"Key: {pair.Data} ({typeof(TKey)})");
                }
                else
                {
                    keys.Add(pair.Data);
                }
            }
#endif
        }

        // Override GetEnumerator to return distinct pairs based on TData
        public override IEnumerator<ObjectPair<TKey, TValue>> GetEnumerator()
        {
            return DistinctItems.GetEnumerator();
        }

        public bool ContainsKey(TKey key) => DistinctItems.Any(pair => EqualityComparer<TKey>.Default.Equals(pair.Data, key));

        public IEnumerable<TKey> Keys => DistinctItems.Select(pair => pair.Data);

        public IEnumerable<TValue> Values => DistinctItems.Select(pair => pair.Value);

        /// <summary>
        /// Similar to <see cref="ObjectPairCollection{TData,TValue}.this[TData]"/>, but returns bool
        /// </summary>
        public bool TryGetValue(TKey key, out TValue value)
        {
            var pair = DistinctItems.FirstOrDefault(p => EqualityComparer<TKey>.Default.Equals(p.Data, key));
            if (pair != null)
            {
                value = pair.Value;
                return true;
            }
            value = default;
            return false;
        }

        public override void Add(TKey data, TValue value)
        {
            if (!ContainsKey(data))
            {
                base.Add(data, value);
            }
            else
            {
                LogWarning($"Duplicate key found: {data}. Entry will not be added.");
            }
        }

        public int RemoveWhere(Func<ObjectPair<TKey, TValue>, bool> predicate)
        {
            var pairsToRemove = items.Where(predicate).ToList();
            foreach (var pair in pairsToRemove)
            {
                items.Remove(pair);
            }
            return pairsToRemove.Count;
        }

        public void Sort() => items.Sort();
    }

    /// <summary>
    /// A scriptable object containing <see cref="ObjectPairDictionary{TKey,TValue}"/>
    /// </summary>
    /// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
    /// <remarks>
    /// If there's a duplicate items set from the inspector, it will only warn you.
    /// Only unique pairs will get enumerated. 
    /// </remarks>
    public abstract class ObjectPairDictionarySO<TKey, TValue> : ScriptableObjectWithLogger, IEnumerable<ObjectPair<TKey, TValue>>
    {
        [SerializeField] protected ObjectPairDictionary<TKey, TValue> dictionary = new();

        public IEnumerator<ObjectPair<TKey, TValue>> GetEnumerator()
        {
            return dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    /// <summary>
    /// A generic list that pairs elements of type T with float values constrained between 0 and 1. 
    /// </summary>
    [Serializable]
    public class ObjectFloat01List<T> : ObjectPairList<T, float>, ISerializationCallbackReceiver
    {
        public ObjectFloat01List()
        {
        }

        public ObjectFloat01List(IEnumerable<ObjectPair<T, float>> pairs) : base(pairs)
        {
        }

        public override void Add(T data, float value)
        {
            if (value is > 1 or < 0)
            {
                throw new($"{nameof(ObjectFloat01List<T>)} can only accept float value between 0 and 1");
            }
            base.Add(data, value);
        }

        public T GetRandomItem()
        {
            float totalChance = items.Sum(pair => pair.Value);

            float randomValue = Random.value * totalChance;

            foreach (var pair in items.OrderBy(p => Random.value))
            {
                if (randomValue < pair.Value)
                {
                    return pair.Data;
                }
                randomValue -= pair.Value;
            }

            // This should not happen if chances are properly set, but return default value if it does.
            return default;
        }

        public void OnBeforeSerialize()
        {

        }

        public void OnAfterDeserialize()
        {
            for (int i = 0; i < items.Count; i++)
            {
                // Clamp the float value to be within 0 to 1 range
                items[i] = new(items[i].Data, Mathf.Clamp01(items[i].Value));
            }
        }
    }
}