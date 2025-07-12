using RoguelikeCardSystem.Game.Resources.Model;
using UnityEngine;

namespace RogueLikeCardSystem.Game.Actions.Events
{
    public class UpdateResourceEvent<T> : BaseEvent<T>
    {
        public ResourceType Type;
        public int Amount;
    }
}
