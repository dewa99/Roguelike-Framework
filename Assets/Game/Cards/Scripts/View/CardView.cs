using System;
using UnityEngine;
using Coffee.UIEffects;
using Flexalon;
using PrimeTween;
using TMPro;


namespace RogueLikeCardSystem
{
    public partial class CardView : MonoBehaviour
    {
        public event Action OnClickEvent;
        public event Action<bool> OnHoverEvent;
        [SerializeField] private UIEffectPreset selectedEffect, disabledEffect;
        [SerializeField] private TextMeshProUGUI title, cost, effect;
        private FlexalonInteractable interactableElement;
        public bool IsDragging = false;
        public bool CanInteract = true;
        private void Start()
        {
            #region Interactions
            interactableElement = GetComponent<FlexalonInteractable>();
            interactableElement.DragStart.AddListener(x =>
            {
                IsDragging = true;
            });
            interactableElement.DragEnd.AddListener(x =>
            {
                IsDragging = false; 
            });
            interactableElement.HoverStart.AddListener(x =>
            {
               Hover(true);
            });
            interactableElement.HoverEnd.AddListener(x =>
            {
                Hover(false);
            });
            interactableElement.Clicked.AddListener(x =>
            {
                if(CanInteract)
                    OnClickEvent?.Invoke();
            });
            #endregion
        }

        public void Hover(bool state)
        {
            if (!IsDragging && CanInteract && state)
            {
                OnHoverEvent?.Invoke(true);
                Sequence.Create()
                    .Group(Tween.LocalPositionY(this.transform, 10f, 0.3f, Ease.InQuad));
            }
            else if (!IsDragging && CanInteract && !state)
            {
                Sequence.Create()
                    .Group(Tween.LocalPositionY(this.transform, 00f, 0.3f, Ease.InQuad))
                    .Group(Tween.LocalRotation(this.transform, Vector3.zero, 0.3f, Ease.InQuad));
            }
        }
        public void UpdateView(string title, string resourceType, string cost, string effect)
        {
            this.title.text = title;
            this.cost.text = $"{cost} {resourceType}";
            this.effect.text = effect;
        }

    }
}
