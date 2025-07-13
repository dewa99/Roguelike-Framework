using Cysharp.Threading.Tasks;
using RoguelikeCardSystem.Game.Resources.Model;
using RoguelikeCardSystem.Game.Resources.View;

namespace RoguelikeCardSystem.Game.Resources.Presenter
{
    public class ResourcesPresenter : IResourcesPresenter
    {
        public ResourcesModel model { get; private set; }
        public ResourcesView view { get; private set; }

        public ResourcesPresenter(ResourcesModel model, ResourcesView view)
        {
            this.model = model;
            this.view = view;
        }

        public async UniTask UpdateResource(ResourceType type, int amount)
        {
            model.UpdateResource(type, model.GetResource(type) + amount);
            await view.UpdateResource(type, model.GetResource(type), model.GetResource(type) + amount);
        }

        public int GetResource(ResourceType type)
        {
            return model.GetResource(type);
        }
    }
}