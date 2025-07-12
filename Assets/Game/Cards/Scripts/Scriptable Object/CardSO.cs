using System.Collections.Generic;
using UnityEngine;
using SerializeReferenceEditor;
using NaughtyAttributes; 

namespace RogueLikeCardSystem
{
    [CreateAssetMenu(fileName = "CardSO", menuName = "Scriptable Objects/CardSO")]
    public class CardSO : ScriptableObject
    {
        [BoxGroup("Card Data")]
        public CardData Data;
    }
}

