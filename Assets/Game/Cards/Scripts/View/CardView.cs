using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Coffee.UIEffects;
using Flexalon;
using PrimeTween;

namespace RogueLikeCardSystem
{
    public class CardView : MonoBehaviour
    {
        public event Action OnClickEvent;
        public event Action<bool> OnHoverEvent;
        [SerializeField] private UIEffectPreset selectedEffect, disabledEffect;
        private FlexalonInteractable interactableElement;
        bool isDragging = false;
        bool canInteract = true;
        private void Start()
        {
            #region Interactions
            interactableElement = GetComponent<FlexalonInteractable>();
            interactableElement.DragStart.AddListener(x =>
            {
                isDragging = true;
            });
            interactableElement.DragEnd.AddListener(x =>
            {
                isDragging = false; 
            });
            interactableElement.HoverStart.AddListener(x =>
            {
                if (!isDragging)
                {
                    OnHoverEvent?.Invoke(true);
                    Sequence.Create()
                        .Group(Tween.LocalPositionY(this.transform, 10f, 0.3f, Ease.InQuad));
                }
            });
            interactableElement.HoverEnd.AddListener(x =>
            {
                if (!isDragging)
                {
                    Sequence.Create()
                        .Group(Tween.LocalPositionY(this.transform, 00f, 0.3f, Ease.InQuad))
                        .Group(Tween.LocalRotation(this.transform, Vector3.zero, 0.3f, Ease.InQuad));
                }
            });
            interactableElement.Clicked.AddListener(x =>
            {
                if(canInteract)
                    OnClickEvent?.Invoke();
            });
            #endregion
        }

        public void UpdateData()
        {

        }

    }
}
