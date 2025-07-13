using Cysharp.Threading.Tasks;
using PrimeTween;
using RoguelikeCardSystem.Game.Resources.Model;
using TMPro;
using UnityEngine;

namespace RoguelikeCardSystem.Game.Resources.View
{
    public class ResourcesView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;

        public async UniTask UpdateResource(ResourceType type ,int current, int amount)
        {
            await AnimateTo(text,type, current, amount);
        }

        public async UniTask AnimateTo(TextMeshProUGUI text, ResourceType type ,int startValue, int endValue)
        {
            Tween.StopAll(this);
            await Tween.Custom(
                startValue,
                endValue,
                duration: 0.5f,
                ease: Ease.OutQuad,
                onValueChange: value => text.text = $"{type} : {(int)value}"
            ); 
        }
    }
}