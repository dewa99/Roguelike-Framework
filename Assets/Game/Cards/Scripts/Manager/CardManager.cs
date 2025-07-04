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
            await DrawCard(evt.Amount);
            evt.Response.Respond(true);
        });

        MessageBroker.Default.Receive<PlayEvent<string>>().Subscribe(async evt =>
        {
            await PlayCard(evt.Name);
            evt.Response.Respond(evt.Name);
            Debug.Log("Finished played card :" +evt.Name);
        });
    }

    private async void Start()
    {
        Debug.Log("Drawing...");
        var draw = await new DrawAction() { Amount = 5 }.PerformAsync<bool>();
        Debug.Log("Finished Drawing");

        Debug.Log("Play card Miracle");
        var play = await new PlayAction() { Name = "Dewa" }.PerformAsync<string>();
        
    }

    private async UniTask DrawCard(int amount)
    {
        await UniTask.Delay(5000);
    }

    private async UniTask PlayCard(string card)
    {
        await UniTask.Delay(5000);
    }
}
