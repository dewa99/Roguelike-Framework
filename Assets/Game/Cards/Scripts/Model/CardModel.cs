using UnityEngine;
using NaughtyAttributes;
using SerializeReferenceEditor;
using System.Collections.Generic;
using RogueLikeCardSystem.Game.Actions;
using RoguelikeCardSystem.Game.Resources.Manager;
using RoguelikeCardSystem.Game.Resources.Model;

namespace RogueLikeCardSystem
{
    
    public class CardModel
    {
        public CardData data;
        public CardModel(CardSO cardSO)
        {
            data = cardSO.Data;
        }
    }

    [System.Serializable]
    public class CardData
    {
        public string Name;
        [TextArea]
        public string DescriptionText;
        public int MaxUsage;
        public ResourceType ResourceCost;
        public int CostAmount;
        [ShowAssetPreview(256, 256)]
        public Sprite Illustration;
        [Space(5)]
        [SerializeReference]
        [SR]
        public List<BaseAction> PreActions, PlayActions, PlayedActions, DiscardedAction;
    }
}
