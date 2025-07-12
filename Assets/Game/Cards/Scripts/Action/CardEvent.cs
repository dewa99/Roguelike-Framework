using UnityEngine;

namespace RogueLikeCardSystem.Game.Actions.Events
{
    public class DrawEvent<T> : BaseEvent<T>
    {
        public int Amount;
    }

    public class PlayEvent<T> : BaseEvent<T>
    {
        public string Name;
    }
}


