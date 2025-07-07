using System.Collections.Generic;
using UnityEngine;
using SerializeReferenceEditor;

namespace RogueLikeCardSystem
{
    [CreateAssetMenu(fileName = "CardSO", menuName = "Scriptable Objects/CardSO")]
    public class CardSO : ScriptableObject
    {
        [SerializeReference]
        [SR]
        public List<BaseAction> PreActions, PlayActions, PlayedActions, DiscardedAction;
    }
}

