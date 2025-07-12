using Cysharp.Threading.Tasks;
using UniRx;
using RogueLikeCardSystem.Game.Actions;
using RogueLikeCardSystem.Game.Actions.Events;

namespace RogueLikeCardSystem.Game.Cards.Actions
{
    public class DrawAction : BaseAction
    {
        public int Amount;

        public override async UniTask<T> PerformAsync<T>()
        {
            var evt = new DrawEvent<T> { Amount = Amount };
            MessageBroker.Default.Publish(evt);
            return await evt.Response.Task;
        }
    }

    public class PlayAction : BaseAction
    {
        public string Name;

        public override async UniTask<T> PerformAsync<T>()
        {
            var evt = new PlayEvent<T> { Name = Name };
            MessageBroker.Default.Publish(evt);
            return await evt.Response.Task;
        }
    }
}


