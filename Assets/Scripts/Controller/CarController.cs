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


        public void SetCarPathPair(CarPathPair aCarPathPair)
        {
            carPathPair = aCarPathPair;
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
            transform.Rotate(new Vector3(0, 0, -carPathPair.Car.Speed) * Time.deltaTime * carPathPair.Car.Speed, Space.World);
        }

        private void TurnLeft()
        {
            transform.Rotate(new Vector3(0, 0, carPathPair.Car.Speed) * Time.deltaTime * carPathPair.Car.Speed, Space.World);
        }

        public void HandleEvent(GameStatusEvent aEvent)
        {
            if (aEvent is Reset)
            {
                lastResetFrameCount = 0;
                carPathPair.Path.UserInputPerFrames.Clear();
            }
        }

        private void Update()
        {
            transform.Translate(carPathPair.Car.Speed * Time.deltaTime * 0.4f, 0, 0);
        }


    }
}
