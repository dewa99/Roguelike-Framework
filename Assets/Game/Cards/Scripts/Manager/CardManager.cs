using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using RoguelikeCardSystem.Game.Resources.Manager;
using RogueLikeCardSystem.Game.Cards.Model;
using RogueLikeCardSystem.Game.Cards.Presenter;
using RoguelikeCardSystem.Game.Resources.Action;
using UnityEngine;
using UnityUtils;
using Zenject;

namespace RogueLikeCardSystem.Game.Cards.Manager
{
    public enum CardState
    {
        Draw,
        Playing,
        Waiting,
        Discard,
        Remove,
    }
    public partial class CardManager : MonoBehaviour, ICardManager
    {
        [Inject] private readonly IResourcesManager resourceManager;
        [SerializeField] private CardView cardViewPrefab;
        [SerializeField] private CardCollectionSO cardCollection;
        [SerializeField] private Transform handContainer,discardContainer;
        
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
            view.SetInactive();
            return presenter;
        }

        public async UniTask DiscardCard(ICardPresenter card)
        {
            Repository.Repository.CardRepository.Remove(card,CardPileType.Draw);
            Repository.Repository.CardRepository.Add(card,CardPileType.Discard);
            await card.OnMove(discardContainer);
        }

        public async UniTask DiscardCard(int amount)
        {
            for (var i = 0; i < amount; i++)
            {

            }
        }
        
        public async UniTask DiscardCard(Func<ICardPresenter, bool> condition)
        {
            
        }

        public async UniTask DrawCard(ICardPresenter card)
        {
            card.OnClicked += PlayCard;
            Repository.Repository.CardRepository.Add(card,CardPileType.Draw);
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
            await DiscardCard(card);
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


