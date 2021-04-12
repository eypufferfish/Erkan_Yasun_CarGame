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
        private CarPathPair carPathPair;
        [SerializeField]
        private bool isActiveCar;


        private void Start()
        {
        }

        public void SetCarPathPair(CarPathPair aCarPathPair)
        {
            carPathPair = aCarPathPair;
            gameObject.GetComponent<Rigidbody2D>().velocity = transform.up * carPathPair.Car.Speed;
        }


        public void HandleEvent(UserInputEvent aEvent)
        {
            if (aEvent is TurnLeft)
            {
                TurnLeft();
            }
            else if (aEvent is TurnRight)
            {
                TurnRight();
            }

            if (isActiveCar)
            {
                carPathPair.Path.UserInputPerFrames.Add(lastResetFrameCount, aEvent);
            }
        }

        private void TurnRight()
        {
            transform.Rotate(0.0f, 90.0f, 0.0f);
        }

        private void TurnLeft()
        {
            transform.Rotate(0.0f, -90.0f, 0.0f);
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
