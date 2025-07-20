using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Coffee.UIEffects;
using Cysharp.Threading.Tasks;
using Flexalon;
using PrimeTween;
using UnityEngine.Serialization;

namespace RogueLikeCardSystem
{
    public class CardView : MonoBehaviour
    {
        public event Action OnClickEvent;
        public event Action<bool> OnHoverEvent;
        [SerializeField] private UIEffectPreset selectedEffect, disabledEffect;
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
                if (!IsDragging)
                {
                    OnHoverEvent?.Invoke(true);
                    Sequence.Create()
                        .Group(Tween.LocalPositionY(this.transform, 10f, 0.3f, Ease.InQuad));
                }
            });
            interactableElement.HoverEnd.AddListener(x =>
            {
                if (!IsDragging)
                {
                    Sequence.Create()
                        .Group(Tween.LocalPositionY(this.transform, 00f, 0.3f, Ease.InQuad))
                        .Group(Tween.LocalRotation(this.transform, Vector3.zero, 0.3f, Ease.InQuad));
                }
            });
            interactableElement.Clicked.AddListener(x =>
            {
                if(CanInteract)
                    OnClickEvent?.Invoke();
            });
            #endregion
        }

        public void UpdateData()
        {

        }

        public async UniTask RunPlayAnimation()
        {
            await Sequence.Create()
                .Group(Tween.ShakeLocalPosition(transform, new Vector3(5f,5f,5f) ,1f, 0.3f));
        }
    }
}
