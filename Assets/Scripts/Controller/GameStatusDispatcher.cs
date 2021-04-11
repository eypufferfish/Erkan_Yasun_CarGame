using Mobge.CarGame.ErkanYasun.Data.Event.GameStatus;
using System.Collections.Generic;
using UnityEngine;

namespace Mobge.CarGame.ErkanYasun.Controller
{
    public class GameStatusDispatcher : MonoBehaviour, IEventDispatcher<GameStatusEvent>
    {
        [SerializeField]
        private List<IEventListener<GameStatusEvent>> listeners = new List<IEventListener<GameStatusEvent>>();

        public void IEventDispatcher<GameStatusEvent>.RegisterListener(IEventListener<GameStatusEvent> aListener)
        {
            listeners.Add(aListener);
        }

        public void IEventDispatcher<GameStatusEvent>.UnRegisterListener(IEventListener<GameStatusEvent> aListener)
        {
            listeners.Add(aListener);
        }

        void IEventDispatcher<GameStatusEvent>.DispatchEvent(GameStatusEvent aEvent)
        {
            foreach (IEventListener<GameStatusEvent> listener in listeners)
            {
                listener.HandleEvent(aEvent);
            }
        }
    }
}
