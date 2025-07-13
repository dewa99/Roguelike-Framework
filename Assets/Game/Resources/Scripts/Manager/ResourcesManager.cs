using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using RoguelikeCardSystem.Game.Resources.Action;
using RoguelikeCardSystem.Game.Resources.Model;
using RoguelikeCardSystem.Game.Resources.Presenter;
using RoguelikeCardSystem.Game.Resources.View;
using RogueLikeCardSystem.Game.Actions.Events;
using UniRx;
using UnityEngine;
using NaughtyAttributes;
using RogueLikeCardSystem;
using RoguelikeCardSystem.Game.Utilities;

namespace RoguelikeCardSystem.Game.Resources.Manager
{
    public class ResourcesManager : MonoBehaviour, IResourcesManager
    {
        private IResourcesPresenter presenter;
        [SerializeField] private ResourcesView view;
        [SerializeField] private ResourceListSO starterResource;

        private void Start()
        {
            Initialize();
        }
        public void Initialize()
        {
            Dictionary<ResourceType,int> resources = new();
            foreach (var resource in starterResource.ResourceList)
            {
                resources.Add(resource.Data, resource.Value);
            }
            
            var model = new ResourcesModel(resources);
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
        
        public int GetResource(ResourceType type)
        {
            return presenter.GetResource(type);
        }
    }

}