using System;
using Cysharp.Threading.Tasks;
using RogueLikeCardSystem.Game.Cards.Model;
using RogueLikeCardSystem.Game.Interfaces;
using RogueLikeCardSystem.Game.Utilities;
using UnityEngine;


namespace RogueLikeCardSystem
{
    public interface ICardManager : IManager
    {
        ICardPresenter CreateCard(CardSO card, Transform parent);
        UniTask DrawCard(ICardPresenter card);
        UniTask DrawCard(int amount);
        UniTask DrawCard(Func<ICardPresenter, bool> condition);
        UniTask PlayCard(ICardPresenter card);
        UniTask DiscardCard(ICardPresenter card);
        UniTask DiscardCard(int amount);
        UniTask DiscardCard(Func<ICardPresenter, bool> condition);
        UniTask RemoveCard(ICardPresenter card);
        UniTask RemoveCard(int amount);
        StatModifier<int> AddStatModifier(ICardPresenter card, int amount, StatModifierType type, CardStatType stat);
        void RemoveStatModifier(ICardPresenter card, CardStatType stat, StatModifier<int> modifier);
        void ClearStatModifier(ICardPresenter card, CardStatType stat);
    }
}
