using Mobge.CarGame.ErkanYasun.Model.Event.GameStatus;
using Mobge.CarGame.ErkanYasun.Model.Event.UserInput;
using UnityEngine;

namespace Mobge.CarGame.ErkanYasun.Controller
{
    public class UserInputController : MonoBehaviour, IEventListener<GameStatusEvent>
    {
        private GameStatusEvent currentGameStatus;
        public GameStatusController GameStatusController { get; set; }

        public UserInputEventDispatcher UserInputEventDispatcher { get; set; }

        public void Awake()
        {
            UserInputEventDispatcher = ScriptableObject.CreateInstance<UserInputEventDispatcher>();
        }

        public void Update()
        {
            if ((currentGameStatus == null || currentGameStatus is FinishLevel) && Input.touchCount > 0)
            {
                GameStatusController.StartLevel();
            }
        }

        public void Turn(UserInputEvent aUserInputEvent)
        {
            if (!(currentGameStatus == null || currentGameStatus is FinishLevel))
            {
                UserInputEventDispatcher.DispatchEvent(aUserInputEvent);
            }
        }

        public void HandleEvent(GameStatusEvent aEvent)
        {
            currentGameStatus = aEvent;
        }
    }
}