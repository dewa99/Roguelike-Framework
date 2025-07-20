using RogueLikeCardSystem.Game.Cards.Manager;
using RoguelikeCardSystem.Game.Resources.Manager;
using UnityEngine;
using Zenject;


namespace RogueLikeCardSystem
{
    [CreateAssetMenu(fileName = "ManagerInstaller", menuName = "Installers/ManagerInstaller")]
    public class ManagerInstaller : ScriptableObjectInstaller<ManagerInstaller>
    {
        public override void InstallBindings()
        {
            Container.TryBindFromComponentInHierarchy<ICardManager,CardManager>();
            Container.TryBindFromComponentInHierarchy<IResourcesManager,ResourcesManager>();
        }
    }
}