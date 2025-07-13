using System.Collections.Generic;
using UnityEngine;

namespace RogueLikeCardSystem
{
    [CreateAssetMenu(fileName = "CardCollectionSO", menuName = "Scriptable Objects/CardCollectionSO")]
    public class CardCollectionSO : ScriptableObject
    {
        public List<CardSO> cards;  
    }
}
