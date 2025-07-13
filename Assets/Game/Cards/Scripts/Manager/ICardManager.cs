using System;
using Cysharp.Threading.Tasks;
using RogueLikeCardSystem.Game.Interfaces;


namespace RogueLikeCardSystem
{
    public interface ICardManager : IManager
    {
        ICardPresenter CreateCard(CardSO card);
        UniTask DrawCard(ICardPresenter card);
        UniTask DrawCard(int amount);
        UniTask DrawCard(Func<ICardPresenter, bool> condition);
        UniTask PlayCard(ICardPresenter card);
        UniTask DiscardCard(ICardPresenter card);
        UniTask DiscardCard(int amount);
        UniTask DiscardCard(Func<ICardPresenter, bool> condition);
        UniTask RemoveCard(ICardPresenter card);
        UniTask RemoveCard(int amount);
    }
}
