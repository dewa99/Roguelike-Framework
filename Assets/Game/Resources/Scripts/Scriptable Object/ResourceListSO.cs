using RoguelikeCardSystem.Game.Resources.Model;
using RoguelikeCardSystem.Game.Utilities;
using UnityEngine;

namespace RogueLikeCardSystem
{
    [CreateAssetMenu(fileName = "ResourceListSO", menuName = "Scriptable Objects/ResourceListSO")]
    public class ResourceListSO : ScriptableObject
    {
        public ObjectPairList<ResourceType, int> ResourceList;
    }
}
