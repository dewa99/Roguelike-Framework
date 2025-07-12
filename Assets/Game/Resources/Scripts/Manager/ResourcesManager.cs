using Cysharp.Threading.Tasks;
using RoguelikeCardSystem.Game.Resources.Action;
using RoguelikeCardSystem.Game.Resources.Model;
using RoguelikeCardSystem.Game.Resources.Presenter;
using RoguelikeCardSystem.Game.Resources.View;
using RogueLikeCardSystem.Game.Actions.Events;
using UniRx;
using UnityEngine;
using NaughtyAttributes;

namespace RoguelikeCardSystem.Game.Resources.Manager
{
    public class ResourcesManager : MonoBehaviour, IResourcesManager
    {
        private IResourcesPresenter presenter;
        [SerializeField] private ResourcesView view;

        private void Start()
        {
            Initialize();
        }
        public void Initialize()
        {
            var model = new ResourcesModel();
            presenter = new ResourcesPresenter(model, view);
            #region Event bus
            MessageBroker.Default.Receive<UpdateResourceEvent<bool>>().Subscribe(async evt =>
            {
                await UpdateResource(evt.Type,evt.Amount);
                evt.Response.Respond(true);
            });
            #endregion
            
        }

        public async UniTask UpdateResource(ResourceType type, int amount)
        {
            await presenter.UpdateResource(type, amount);
            
        }
        [Button]
        public async UniTask UpdateResource()
        {
            Debug.Log("Start counting");
            var update = await new UpdateResource() { Amount = 100, Type = ResourceType.Crystal }.PerformAsync<bool>();
            Debug.Log("Finish counting");
        }
    }

}