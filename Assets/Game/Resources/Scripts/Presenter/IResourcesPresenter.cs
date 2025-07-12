using Cysharp.Threading.Tasks;
using RoguelikeCardSystem.Game.Resources.Manager;
using RoguelikeCardSystem.Game.Resources.Model;

namespace RoguelikeCardSystem.Game.Resources.Presenter
{
    public interface IResourcesPresenter
    {
        UniTask UpdateResource(ResourceType type, int amount);
    }
}