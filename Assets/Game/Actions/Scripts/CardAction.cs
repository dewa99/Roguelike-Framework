using Cysharp.Threading.Tasks;
using UnityEngine;
using UniRx;
public class DrawAction : BaseAction
{
    public int amount;

    public override async UniTask<TResult> PerformAsync<TResult>()
    {
        var response = new RequestResponse<TResult>();
        var evt = new DrawEvent<TResult> { amount = amount , response = response };
        MessageBroker.Default.Publish(evt);
        return await response.tcs.Task;
    }
}
