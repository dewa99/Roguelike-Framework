using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using RoguelikeCardSystem.Game.Resources.Manager;
using RogueLikeCardSystem.Game.Actions.Events;
using RogueLikeCardSystem.Game.Cards.Model;
using RogueLikeCardSystem.Game.Cards.Presenter;
using RoguelikeCardSystem.Game.Resources.Action;
using RogueLikeCardSystem.Game.Utilities;
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
        [SerializeField] private Transform handContainer;
        
        private void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            Subscribe();
            new CardRepository();
        }
        public ICardPresenter CreateCard(CardSO card, Transform parent)
        {
            var view = Instantiate(cardViewPrefab, parent);
            CardModel model = new(card);
            var presenter = new CardPresenter(model,view);
            Repository.Repository.CardRepository.Add(presenter,CardPileType.Draw);
            view.SetInactive();
            return presenter;
        }

        public async UniTask DiscardCard(ICardPresenter card)
        {
            await card.OnDiscard();
        }

        public async UniTask DiscardCard(int amount)
        {
            for (var i = 0; i < amount; i++)
            {
                await Repository.Repository.CardRepository.Get(CardPileType.Hand).First().OnDiscard();
            }
        }
        
        public async UniTask DiscardCard(Func<ICardPresenter, bool> condition)
        {
            
        }

        public async UniTask DrawCard(ICardPresenter card)
        {
            card.OnClicked += PlayCard;
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
            await new UpdateResource(){Type = card.Model.data.ResourceCost, Amount = -card.Model.data.ResourceCostValue}.PerformAsync<bool>();
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
        public async UniTask CreateCard()
        {
            var card = CreateCard(cardCollection.cards.First(),handContainer);
            await DrawCard(card);
        }
    }
}


