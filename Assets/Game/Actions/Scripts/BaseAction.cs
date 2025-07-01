using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

public abstract class BaseAction
{
    public abstract UniTask<TResponse> PerformAsync<TResponse>();
}

public class RequestResponse<T>
{
    public readonly UniTaskCompletionSource<T> tcs = new();

    public void Respond(T value)
    {
        tcs.TrySetResult(value);
    }
}
