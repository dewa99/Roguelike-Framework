using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace RogueLikeCardSystem.Game.Cards.Presenter
{
    public class CardPresenter : ICardPresenter
    {
        private CardModel model;
        private CardView view;
        public event Action<ICardPresenter> OnClicked;
        public event Action<ICardPresenter, bool> OnHovered;

        public CardPresenter(CardModel model, CardView view)
        {
            this.model = model;
            this.view = view;
            view.OnClickEvent += OnClick;
            view.OnHoverEvent += OnHover;
        }

        public void OnClick()
        {
            OnClicked?.Invoke(this);
        }

        public void OnHover(bool state)
        {
            OnHovered?.Invoke(this, state);
        }

        public async UniTask OnDraw()
        {
            throw new System.NotImplementedException();
        }

        public async UniTask OnDiscard()
        {
            throw new System.NotImplementedException();
        }

        public async UniTask OnPlay()
        {
            await UniTask.Delay(1000);
        }
    }
}
