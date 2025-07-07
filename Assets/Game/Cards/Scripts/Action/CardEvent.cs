using UnityEngine;

namespace RogueLikeCardSystem
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


