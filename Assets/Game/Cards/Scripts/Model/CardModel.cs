using UnityEngine;
using NaughtyAttributes;
using SerializeReferenceEditor;
using System.Collections.Generic;
using RogueLikeCardSystem.Game.Actions;
using RoguelikeCardSystem.Game.Resources.Model;
using RogueLikeCardSystem.Game.Utilities;

namespace RogueLikeCardSystem.Game.Cards.Model
{
    public class CardModel
    {
        public CardData data;
        public CardModel(CardSO cardSO)
        {
           data = new(cardSO.Data);
        }
    }

    [System.Serializable]
    public class CardData
    {
        public string Name;
        [TextArea]
        public string DescriptionText;
        public Stat<int> MaxUsage;
        public ResourceType ResourceCost;
        public Stat<int> CostAmount;
        [ShowAssetPreview(256, 256)]
        public Sprite Illustration;
        [Space(5)]
        [SerializeReference]
        [SR]
        public List<BaseAction> PreActions, PlayActions, PlayedActions, DiscardedAction;

        public CardData(CardData data)
        {
            Name = data.Name;
            DescriptionText = data.DescriptionText;
            MaxUsage = new (data.MaxUsage.BaseValue);
            ResourceCost = data.ResourceCost;
            Illustration = data.Illustration;
            CostAmount = new (data.CostAmount.BaseValue);
            PreActions = new (data.PreActions);
            PlayActions = new (data.PlayActions);
            PlayedActions = new(data.PlayedActions);
            DiscardedAction = new (data.DiscardedAction);
        }
    }
}
