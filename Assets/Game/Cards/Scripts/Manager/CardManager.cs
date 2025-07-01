using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

public partial class CardManager : MonoBehaviour
{
    private void OnEnable()
    {
        MessageBroker.Default.Receive<DrawEvent<bool>>().Subscribe(async evt =>
        {
            await DrawCard(evt.amount);
            evt.response.Respond(true);
        });
    }

    private async void Start()
    {
        Debug.Log("Drawing...");
        var draw = await new DrawAction() { amount = 5 }.PerformAsync<bool>();
        Debug.Log("Finished Drawing");
    }

    private async UniTask DrawCard(int amount)
    {

        await UniTask.Delay(5000);
    }
}
