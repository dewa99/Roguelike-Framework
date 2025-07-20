using System;
using Cysharp.Threading.Tasks;
using RogueLikeCardSystem.Game.Cards.Model;
using RogueLikeCardSystem.Game.Utilities;
using UnityEngine;

namespace RogueLikeCardSystem
{
    public interface ICardPresenter
    {
        CardModel Model {get; set;}
        CardView View {get; set;}
        event Func<ICardPresenter, UniTask> OnClicked;
        event Action<ICardPresenter, bool> OnHovered;
        void OnClick();
        void OnHover(bool state);
        UniTask OnDraw();
        UniTask OnDiscard();
        UniTask OnPlay();
        StatModifier<int> AddModifier(int amount, StatModifierType type,CardStatType stat);
        void RemoveModifier(CardStatType stat, StatModifier<int> modifier);
        void ClearModifier(CardStatType type);
    }
}
