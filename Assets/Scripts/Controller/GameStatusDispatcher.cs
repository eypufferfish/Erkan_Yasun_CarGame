using Mobge.CarGame.ErkanYasun.Model.Event.GameStatus;
using System.Collections.Generic;
using UnityEngine;

namespace Mobge.CarGame.ErkanYasun.Controller
{
    public class GameStatusDispatcher : MonoBehaviour, IEventDispatcher<GameStatusEvent>
    {
        [SerializeField]
        private List<IEventListener<GameStatusEvent>> listeners = new List<IEventListener<GameStatusEvent>>();

        public void RegisterListener(IEventListener<GameStatusEvent> aListener)
        {
            listeners.Add(aListener);
        }

        public void UnRegisterListener(IEventListener<GameStatusEvent> aListener)
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
