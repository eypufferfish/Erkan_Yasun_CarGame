using Mobge.CarGame.ErkanYasun.Model.Event.UserInput;
using System.Collections.Concurrent;
using UnityEngine;

namespace Mobge.CarGame.ErkanYasun.Controller
{
    public class UserInputEventDispatcher : ScriptableObject, IEventDispatcher<UserInputEvent>
    {
        [SerializeField]
        private readonly ConcurrentBag<IEventListener<UserInputEvent>> listeners = new ConcurrentBag<IEventListener<UserInputEvent>>();

        public void RegisterListener(IEventListener<UserInputEvent> aListener)
        {
            listeners.Add(aListener);
        }

        public void UnRegisterListener(IEventListener<UserInputEvent> aListener)
        {
            listeners.Add(aListener);
        }

        public void DispatchEvent(UserInputEvent aEvent)
        {
            foreach (IEventListener<UserInputEvent> listener in listeners)
            {
                listener.HandleEvent(aEvent);
            }
        }
    }
}