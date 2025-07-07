using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace RogueLikeCardSystem
{
    [System.Serializable]
    public abstract class BaseAction
    {
        public abstract UniTask<T> PerformAsync<T>();
    }

    public class RequestResponse<T>
    {
        private readonly UniTaskCompletionSource<T> _tcs = new();

        // Public task accessor
        public UniTask<T> Task => _tcs.Task;

        public void Respond(T value) => _tcs.TrySetResult(value);
    }
}


