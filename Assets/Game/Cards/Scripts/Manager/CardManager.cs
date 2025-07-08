using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace RogueLikeCardSystem
{
    public partial class CardManager : MonoBehaviour, ICardManager
    {
        public void Initialize()
        {
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
        public UniTask CreateCard(CardSO card)
        {
            throw new NotImplementedException();
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
    }
}


