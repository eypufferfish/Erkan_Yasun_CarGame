using Mobge.CarGame.ErkanYasun.Data.Event.UserInput;
using System.Collections.Generic;
using UnityEngine;

namespace Mobge.CarGame.ErkanYasun.Controller
{
    public class UserInputEventDispatcher : MonoBehaviour, IEventDispatcher<UserInputEvent>
    {
        [SerializeField]
        private readonly List<IEventListener<UserInputEvent>> listeners = new List<IEventListener<UserInputEvent>>();

        public void  RegisterListener(IEventListener<UserInputEvent> aListener)
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
