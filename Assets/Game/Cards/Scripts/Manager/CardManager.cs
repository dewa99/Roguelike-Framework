using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using RoguelikeCardSystem.Game.Resources.Manager;
using RogueLikeCardSystem.Game.Actions.Events;
using RogueLikeCardSystem.Game.Cards.Presenter;
using UniRx;
using UnityEngine;
using UnityUtils;
using Zenject;

namespace RogueLikeCardSystem.Game.Cards.Manager
{
    public partial class CardManager : MonoBehaviour, ICardManager
    {
        [Inject] private readonly IResourcesManager resourceManager;
        [SerializeField] private CardView cardViewPrefab;
        [SerializeField] private CardCollectionSO cardCollection;

        private void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            #region  Repository Initialization

            new CardRepository();

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
            var view = Instantiate(cardViewPrefab);
            CardModel model = new(card);
            var presenter = new CardPresenter(model,view);
            Repository.Repository.CardRepository.Add(presenter,CardPileType.Draw);
            return presenter;
        }

        public async UniTask DiscardCard(ICardPresenter card)
        {
            await card.OnDiscard();
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
            await card.OnDraw();
        }

        public async UniTask DrawCard(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                var card = Repository.Repository.CardRepository.Get(CardPileType.Draw).Random();
                await DrawCard(card);
            }
        }

        public async UniTask DrawCard(Func<ICardPresenter, bool> condition)
        {
            throw new NotImplementedException();
        }

        public async UniTask PlayCard(ICardPresenter card)
        {
            await card.OnPlay();
        }

        public async UniTask RemoveCard(ICardPresenter card)
        {
            throw new NotImplementedException();
        }

        public async UniTask RemoveCard(int amount)
        {   
            throw new NotImplementedException();
        }
        
    }
}


