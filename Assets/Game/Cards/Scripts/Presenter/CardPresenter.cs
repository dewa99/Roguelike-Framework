using UnityEngine;

namespace RogueLikeCardSystem.Game.Cards.Presenter
{
    public class CardPresenter : ICardPresenter
    {
        private CardModel model;
        private CardView view;

        public CardPresenter(CardModel model, CardView view)
        {
            this.model = model;
            this.view = view;
        }
    }
}
