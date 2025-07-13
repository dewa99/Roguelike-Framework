using Cysharp.Threading.Tasks;
using RoguelikeCardSystem.Game.Resources.Model;
using RogueLikeCardSystem;
using RogueLikeCardSystem.Game.Interfaces;

namespace RoguelikeCardSystem.Game.Resources.Manager
{
    public interface IResourcesManager : IManager
    {
        UniTask UpdateResource(ResourceType type ,int amount);
        int GetResource(ResourceType type);
    }
}