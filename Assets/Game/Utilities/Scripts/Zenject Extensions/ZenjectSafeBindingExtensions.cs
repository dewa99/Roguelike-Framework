using UnityEngine;
using Zenject;

namespace RogueLikeCardSystem
{
    public static class ZenjectSafeBindingExtensions
    {
        public static void TryBindFromComponentInHierarchy<TContract, TConcrete>(this DiContainer container, bool asSingle = true)
            where TConcrete : Component, TContract
        {
            var instance = UnityEngine.Object.FindObjectOfType<TConcrete>();
            if (instance != null)
            {
                if (asSingle)
                    container.Bind<TContract>().FromInstance(instance).AsSingle();
                else
                    container.Bind<TContract>().FromInstance(instance).AsCached();
            }
            else
            {
                Debug.LogWarning($"[Zenject] Skipped binding for {typeof(TConcrete).Name} — not found in hierarchy.");
            }
        }
    }
}
