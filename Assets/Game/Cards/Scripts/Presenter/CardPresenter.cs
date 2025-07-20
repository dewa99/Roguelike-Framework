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
        public event Func<ICardPresenter, UniTask> OnClicked;
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
            
        }

        public async UniTask OnDiscard()
        {
            
        }

        public async UniTask OnPlay()
        {
            Model.data.PlayConditions.ForEach(x =>
            {
                if (!x.Check())
                {
                    return;
                }
            });
            Model.data.PreActions.ForEach(async x =>
            {
                await x.PerformAsync<bool>();
            });
            Model.data.PlayActions.ForEach(async x =>
            {
                await x.PerformAsync<bool>();
            });
            Model.data.PlayedActions.ForEach(async x =>
            {
                await x.PerformAsync<bool>();
            });
            
            await UniTask.CompletedTask;
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
