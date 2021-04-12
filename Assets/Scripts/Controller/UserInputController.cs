using Mobge.CarGame.ErkanYasun.Model.Event.UserInput;
using UnityEngine;


namespace Mobge.CarGame.ErkanYasun.Controller
{
    public class UserInputController : MonoBehaviour
    {
        private UserInputEvent turnLeft;
        private UserInputEvent turnRight;

        public UserInputEventDispatcher UserInputEventDispatcher { get; set; }

        private void Awake()
        {
            UserInputEventDispatcher = ScriptableObject.CreateInstance<UserInputEventDispatcher>();
            turnLeft = ScriptableObject.CreateInstance<TurnLeft>();
            turnRight = ScriptableObject.CreateInstance<TurnRight>();
        }

        private void Update()
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
    }
}
