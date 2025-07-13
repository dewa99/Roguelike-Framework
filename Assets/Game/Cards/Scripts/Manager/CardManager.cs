using System;
using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using RoguelikeCardSystem.Game.Resources.Manager;
using RogueLikeCardSystem.Game.Actions.Events;
using RogueLikeCardSystem.Game.Cards.Presenter;
using UniRx;
using UnityEngine;
using Zenject;

namespace RogueLikeCardSystem
{
    public partial class CardManager : MonoBehaviour, ICardManager
    {
        [Inject] private readonly IResourcesManager resourceManager;
        public void Initialize()
        {
            #region  Repository Initialization

            Repository.Repository.CardRepository = new CardRepository();

            #endregion
            #region Event Bus
            MessageBroker.Default.Receive<DrawEvent<bool>>().Subscribe(async evt =>
            {
                await DrawCard(evt.Amount);
                evt.Response.Respond(true);
            });

            MessageBroker.Default.Receive<PlayEvent<string>>().Subscribe(async evt =>
            {
                await PlayCard(null);
                evt.Response.Respond(evt.Name);
            });
            #endregion
        }
        public ICardPresenter CreateCard(CardSO card)
        {
            var presenter = new CardPresenter(null,null);
            Repository.Repository.CardRepository.Add(presenter,CardPileType.Draw);
            return null;
        }

        public async UniTask DiscardCard(ICardPresenter card)
        {
            throw new NotImplementedException();
        }

        public async UniTask DiscardCard(int amount)
        {
            throw new NotImplementedException();
        }

        public async UniTask DiscardCard(Func<ICardPresenter, bool> condition)
        {
            throw new NotImplementedException();
        }

        public async UniTask DrawCard(ICardPresenter card)
        {
            throw new NotImplementedException();
        }

        public async UniTask DrawCard(int amount)
        {
            throw new NotImplementedException();
        }

        public async UniTask DrawCard(Func<ICardPresenter, bool> condition)
        {
            throw new NotImplementedException();
        }

        public async UniTask PlayCard(ICardPresenter card)
        {
            throw new NotImplementedException();
        }

        public async UniTask RemoveCard(ICardPresenter card)
        {
            throw new NotImplementedException();
        }

        public async UniTask RemoveCard(int amount)
        {   
            throw new NotImplementedException();
        }
        [Button]
        public void GetResourceValue()
        {
            Debug.Log(resourceManager.GetResource(RoguelikeCardSystem.Game.Resources.Model.ResourceType.Crystal));
        }
    }
}


