using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using RogueLikeCardSystem.Game.Cards.Model;
using RogueLikeCardSystem.Game.Utilities;
using UnityEngine;

namespace RogueLikeCardSystem.Game.Cards.Presenter
{
    public class CardPresenter : ICardPresenter
    {
        public CardModel Model {get; set;}
        public CardView View {get; set;}
        public event Action<ICardPresenter> OnClicked;
        public event Action<ICardPresenter, bool> OnHovered;

        public CardPresenter(CardModel model, CardView view)
        {
            this.Model = model;
            this.View = view;
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

        public StatModifier<int> AddModifier(int amount, StatModifierType type, CardStatType stat)
        {
            var modifier = new StatModifier<int>(amount, type);
            Model.data.Stats.Where(x => x.Data == stat).FirstOrDefault().Value.AddModifier(modifier);
            return modifier;
        }
        
        public void RemoveModifier(CardStatType type, StatModifier<int> modifier) 
        {
            Model.data.Stats.Where(x => x.Data == type).FirstOrDefault().Value.RemoveModifier(modifier);
        }
        
        public void ClearModifier(CardStatType type) 
        {
            Model.data.Stats.Where(x => x.Data == type).FirstOrDefault().Value.ClearModifiers();
        }
    }
}
