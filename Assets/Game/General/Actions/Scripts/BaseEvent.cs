using RogueLikeCardSystem.Game.Actions;
using UnityEngine;

namespace RogueLikeCardSystem.Game.Actions.Events
{
    public abstract class BaseEvent<T>
    {
        public RequestResponse<T> Response { get; set; } = new();

    }
}

