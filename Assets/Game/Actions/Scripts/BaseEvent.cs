using UnityEngine;

namespace RogueLikeCardSystem
{
    public abstract class BaseEvent<T>
    {
        public RequestResponse<T> Response { get; set; } = new();

    }
}

