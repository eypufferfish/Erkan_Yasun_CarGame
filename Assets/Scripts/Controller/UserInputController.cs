using Mobge.CarGame.ErkanYasun.Model.Event.GameStatus;
using Mobge.CarGame.ErkanYasun.Model.Event.UserInput;
using UnityEngine;

namespace Mobge.CarGame.ErkanYasun.Controller
{
    public class UserInputController : MonoBehaviour, IEventListener<GameStatusEvent>
    {
        private GameStatusEvent currentGameStatus;
        private UserInputEvent turnLeft;
        private UserInputEvent turnRight;
        public GameStatusController GameStatusController { get; set; }

        public UserInputEventDispatcher UserInputEventDispatcher { get; set; }

        public void Awake()
        {
            UserInputEventDispatcher = ScriptableObject.CreateInstance<UserInputEventDispatcher>();
            turnLeft = ScriptableObject.CreateInstance<TurnLeft>();
            turnRight = ScriptableObject.CreateInstance<TurnRight>();
        }

        public void FixedUpdate()
        {
            if (!(currentGameStatus == null || currentGameStatus is FinishLevel))
            {
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    UserInputEventDispatcher.DispatchEvent(turnLeft);
                }
                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    UserInputEventDispatcher.DispatchEvent(turnRight);
                }
            }
            else if (Input.GetKey(KeyCode.Space) && GameStatusController != null)
            {
                GameStatusController.StartLevel();
            }
        }

        public void HandleEvent(GameStatusEvent aEvent)
        {
            currentGameStatus = aEvent;
        }
    }
}