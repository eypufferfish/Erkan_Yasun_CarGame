using Mobge.CarGame.ErkanYasun.Data;
using Mobge.CarGame.ErkanYasun.Data.Event.GameStatus;
using Mobge.CarGame.ErkanYasun.Data.Event.UserInput;
using UnityEngine;


namespace Mobge.CarGame.ErkanYasun.Controller
{
    public class CarReplayController : MonoBehaviour, IEventListener<GameStatusEvent>
    {

        private int lastResetFrameCount = 0;
        [SerializeField]
        private readonly CarPathPair carPathPair;
        private readonly IEventListener<UserInputEvent> carController;

        void IEventListener<GameStatusEvent>.HandleEvent(GameStatusEvent aEvent)
        {
            if (aEvent is Reset)
            {
                lastResetFrameCount = Time.frameCount;
            }
        }

        private void Update()
        {
            int frameIndex = Time.frameCount - lastResetFrameCount;
            carPathPair.Path.UserInputPerFrames.TryGetValue(frameIndex, out UserInputEvent userInputEvent);
            if (userInputEvent != null)
            {
                carController.HandleEvent(userInputEvent);
            }
        }
    }
}
