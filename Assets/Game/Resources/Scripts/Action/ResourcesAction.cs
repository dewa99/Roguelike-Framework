using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using RoguelikeCardSystem.Game.Resources.Model;
using RogueLikeCardSystem.Game.Actions;
using RogueLikeCardSystem.Game.Actions.Events;
using UniRx;

namespace RoguelikeCardSystem.Game.Resources.Action
{
    public class UpdateResource : BaseAction
    {
        public ResourceType Type;
        public int Amount;

        [Button]
        public override async UniTask<T> PerformAsync<T>()
        {
            var evt = new UpdateResourceEvent<T> { Type =  Type, Amount = Amount};
            MessageBroker.Default.Publish(evt);
            return await evt.Response.Task;
        }
    }
}