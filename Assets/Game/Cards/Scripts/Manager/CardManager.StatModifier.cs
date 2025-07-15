using System.Linq;
using RogueLikeCardSystem.Game.Cards.Model;
using RogueLikeCardSystem.Game.Utilities;
using UnityEngine;
using UnityUtils;

namespace RogueLikeCardSystem.Game.Cards.Manager
{
    public partial class CardManager
    {
        public StatModifier<int> AddStatModifier(ICardPresenter card , int amount, StatModifierType type, CardStatType stat)
        {
            return card.AddModifier(amount, type,stat);
        }
        
        public void RemoveStatModifier(ICardPresenter card ,CardStatType stat,StatModifier<int> modifier)
        {
            card.RemoveModifier(stat,modifier);
        }

        public void ClearStatModifier(ICardPresenter card, CardStatType stat)
        {
            card.ClearModifier(stat);
        }
    }
}
