using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using RogueLikeCardSystem.Game.Cards.Model;
using RogueLikeCardSystem.Game.Utilities;
using UnityEngine;
using UnityUtils;

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
            UpdateView();
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
            View.SetActive();
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
            View.CanInteract = false;
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

            await View.RunPlayAnimation();
            await UniTask.CompletedTask;

        }

        public async UniTask OnMove(Transform target)
        {
            await View.RunMoveAnimation(target);
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

        public void UpdateView()
        {
            View.UpdateView(Model.data.Name,Model.data.ResourceCost.ToString(), Model.data.ResourceCostValue.ToString(),Model.data.DescriptionText);
        }
    }
}
