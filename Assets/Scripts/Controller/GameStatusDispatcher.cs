using System.Collections.Concurrent;
using Mobge.CarGame.ErkanYasun.Model.Event.GameStatus;
using UnityEngine;

namespace Mobge.CarGame.ErkanYasun.Controller
{
    public class GameStatusDispatcher : ScriptableObject, IEventDispatcher<GameStatusEvent>
    {
        [SerializeField]
        private readonly ConcurrentBag<IEventListener<GameStatusEvent>> listeners = new ConcurrentBag<IEventListener<GameStatusEvent>>();

        public void RegisterListener(IEventListener<GameStatusEvent> aListener)
        {
            listeners.Add(aListener);
        }

        public void UnRegisterListener(IEventListener<GameStatusEvent> aListener)
        {
            listeners.Add(aListener);
        }

        public void DispatchEvent(GameStatusEvent aEvent)
        {
            foreach (IEventListener<GameStatusEvent> listener in listeners)
            {
                listener.HandleEvent(aEvent);
            }
        }
    }
}