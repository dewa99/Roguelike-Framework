using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Coffee.UIEffects;

namespace RogueLikeCardSystem
{
    public class CardView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {

        public event Action OnClickEvent;
        public event Action<bool> OnHoverEvent;
        private bool canInteract;
        [SerializeField] private UIEffectPreset selectedEffect, disabledEffect; 

        public void UpdateData()
        {

        }
        public void OnPointerClick(PointerEventData eventData)
        {
            if(canInteract)
                OnClickEvent?.Invoke();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (canInteract)
                OnHoverEvent?.Invoke(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            
        }
    }
}
