using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using UnityEngine;

namespace RoguelikeCardSystem.Game.Utilities
{
    public static class MonoBehaviourExtensions
    {
        public static T SafeInvoke<T>(this MonoBehaviour monoBehaviour, Func<T> func)
        {
            // Check if the MonoBehaviour reference is null
            if (monoBehaviour == null)
            {
                return default(T);
            }

            // Invoke the function and return the result
            return func();
        }

        public static void SafeInvoke(this MonoBehaviour monoBehaviour, Action action)
        {
            // Check if the MonoBehaviour reference is null
            if (monoBehaviour == null)
            {
                return;
            }

            // Invoke the action
            action?.Invoke();
        }

        [Pure]
        public static IEnumerable<T> SortByNearest<T>(this IEnumerable<T> list, Vector3 targetPosition) where T : MonoBehaviour
        {
            return list.Where(m => m != null).OrderBy(item => Vector3.Distance(item.transform.position, targetPosition));
        }

        public static bool TryGetComponentInChildren<T>(this MonoBehaviour monoBehaviour, out T result) where T : Component
        {
            result = monoBehaviour != null ? monoBehaviour.GetComponentInChildren<T>() : default;
            return result != default;
        }

        public static bool TryGetComponentsInChildren<T>(this MonoBehaviour monoBehaviour, out T[] results) where T : Component
        {
            results = monoBehaviour != null ? monoBehaviour.GetComponentsInChildren<T>() : Array.Empty<T>();
            return results is { Length: > 0 };
        }

        public static bool IsPrefab(this MonoBehaviour monoBehaviour) => !monoBehaviour.gameObject.scene.IsValid();
    }
}