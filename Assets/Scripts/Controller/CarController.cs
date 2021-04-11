using Mobge.CarGame.ErkanYasun.Data;
using Mobge.CarGame.ErkanYasun.Data.Event.GameStatus;
using Mobge.CarGame.ErkanYasun.Data.Event.UserInput;
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
                carPathPair.Path.UserInputPerFrames.Add(Time.frameCount - lastResetFrameCount, aEvent);
            }
        }

        public void HandleEvent(GameStatusEvent aEvent)
        {
            if (aEvent is Reset)
            {
                lastResetFrameCount = Time.frameCount;
            }
        }
    }
}
