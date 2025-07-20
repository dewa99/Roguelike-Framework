using UnityEngine;
using NaughtyAttributes;
using SerializeReferenceEditor;
using System.Collections.Generic;
using RogueLikeCardSystem.Game.Actions;
using RoguelikeCardSystem.Game.Resources.Model;
using RoguelikeCardSystem.Game.Utilities;
using RogueLikeCardSystem.Game.Utilities;
using UnityUtils;

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
        public ObjectPairList<CardStatType,Stat<int>> Stats;
        public ResourceType ResourceCost;
        public int ResourceCostValue;
        public bool WaitForActionToComplete;
        [ShowAssetPreview(256, 256)]
        public Sprite Illustration;
        [Space(5)]
        [SerializeReference]
        [SR]
        public List<BaseCondition> PlayConditions;
        [Space(5)]
        [SerializeReference]
        [SR]
        public List<BaseAction> PreActions, PlayActions, PlayedActions, DiscardedAction;

        public CardData(CardData data)
        {
            Name = data.Name;
            DescriptionText = data.DescriptionText;

            ObjectPairList<CardStatType, Stat<int>> stats = new();
            data.Stats.ForEach(x =>
            {
                Stat<int> stat = new (x.Value.BaseValue);
                stats.Add(new(x.Data, stat));
            });
            
            Stats = stats;
            ResourceCost = data.ResourceCost;
            Illustration = data.Illustration;
            WaitForActionToComplete = data.WaitForActionToComplete;
            ResourceCostValue = data.ResourceCostValue; 
            PlayConditions = new(data.PlayConditions);
            PreActions = new (data.PreActions);
            PlayActions = new (data.PlayActions);
            PlayedActions = new(data.PlayedActions);
            DiscardedAction = new (data.DiscardedAction);
        }
    }

    public enum CardStatType
    {
        Usage,
        Cost
    }
}
