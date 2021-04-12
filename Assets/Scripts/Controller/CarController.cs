using Mobge.CarGame.ErkanYasun.Model;
using Mobge.CarGame.ErkanYasun.Model.Event.GameStatus;
using Mobge.CarGame.ErkanYasun.Model.Event.UserInput;
using UnityEngine;


namespace Mobge.CarGame.ErkanYasun.Controller
{
    public class CarController : MonoBehaviour, IEventListener<UserInputEvent>, IEventListener<GameStatusEvent>
    {

        private int lastResetFrameCount = 0;
        [SerializeField]
        private readonly CarPathPair carPathPair;
        [SerializeField]
        private readonly bool isActiveCar;

        public void HandleEvent(UserInputEvent aEvent)
        {
            if (isActiveCar)
            {
                carPathPair.Path.UserInputPerFrames.Add(lastResetFrameCount, aEvent);
            }
        }

        public void HandleEvent(GameStatusEvent aEvent)
        {
            if (aEvent is Reset)
            {
                lastResetFrameCount = 0;
                carPathPair.Path.UserInputPerFrames.Clear();
            }
        }




    }
}
