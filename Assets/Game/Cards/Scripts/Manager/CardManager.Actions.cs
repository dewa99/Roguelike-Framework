using RogueLikeCardSystem.Game.Actions.Events;
using UniRx;
using UnityEngine;

namespace RogueLikeCardSystem.Game.Cards.Manager
{
    public partial class CardManager
    {
        public void Subscribe()
        {
            #region Event Bus
            MessageBroker.Default.Receive<DrawEvent<bool>>().Subscribe(async evt =>
            {
                await DrawCard(evt.Amount);
                evt.Response.Respond(true);
            }).AddTo(this);

            MessageBroker.Default.Receive<PlayEvent<string>>().Subscribe(async evt =>
            {
                await PlayCard(null);
                evt.Response.Respond(evt.Name);
            }).AddTo(this);
            #endregion
        }
    }
}
