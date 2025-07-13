using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace RogueLikeCardSystem
{
    public interface ICardPresenter
    {
        event Action<ICardPresenter> OnClicked;
        event Action<ICardPresenter, bool> OnHovered;
        void OnClick();
        void OnHover(bool state);
        UniTask OnDraw();
        UniTask OnDiscard();
        UniTask OnPlay();
    }
}
