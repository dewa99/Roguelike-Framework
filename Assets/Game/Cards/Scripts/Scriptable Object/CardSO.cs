using System.Collections.Generic;
using UnityEngine;
using SerializeReferenceEditor;
using NaughtyAttributes;
using OdinSerializer;
using RogueLikeCardSystem.Game.Cards.Model;

namespace RogueLikeCardSystem
{
    [CreateAssetMenu(fileName = "CardSO", menuName = "Scriptable Objects/CardSO")]
    public class CardSO : ScriptableObject
    {
        [BoxGroup("Card Data")]
        public CardData Data;
    }
}

