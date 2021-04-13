using Mobge.CarGame.ErkanYasun.Model;
using Mobge.CarGame.ErkanYasun.Model.Event.GameStatus;
using Mobge.CarGame.ErkanYasun.Model.Event.UserInput;
using UnityEngine;


namespace Mobge.CarGame.ErkanYasun.Controller
{
    public class CarController : MonoBehaviour, ICarController
    {
        private const string FINISH_TAG = "Finish";
        private const string CAR_TAG = "Car";
        private const string OBSTACLE_TAG = "Obstacle";
        private int frameOffset = 0;
        [SerializeField]
        private CarPathPair carPathPair;
        [SerializeField]
        private bool isReplayMode = false;

        [SerializeField]
        private IGameStatusController gameStatusController;

        public void SetCarPathPair(CarPathPair aCarPathPair)
        {
            carPathPair = aCarPathPair;
        }

        public void SetGameStatusController(GameStatusController aGameStatusController)
        {
            gameStatusController = aGameStatusController;
        }


        public void HandleEvent(UserInputEvent aEvent)
        {
            Debug.Log("Handle User Event:" + aEvent);

            if (!isReplayMode)
            {
                Turn(aEvent);
                RecordUserInput(aEvent);
            }

        }

        private void Turn(UserInputEvent aEvent)
        {
            switch (aEvent)
            {
                case TurnLeft turnLeft:
                    TurnLeft();
                    break;
                case TurnRight turnRight:
                    TurnRight();
                    break;
                default:
                    break;
                case null:
                    throw new System.ArgumentNullException(nameof(aEvent));
            }
        }

        private void RecordUserInput(UserInputEvent aEvent)
        {
            carPathPair.Path.UserInputPerFrames.Add(frameOffset, aEvent);
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
            Debug.Log("Handle Game Status Event:" + aEvent);
            switch (aEvent)
            {
                case StartLevel startLevel:
                    frameOffset = 0;
                    transform.position = carPathPair.Path.Entrance;
                    transform.rotation = Quaternion.Euler(0f, 0f, 90);
                    carPathPair.Path.UserInputPerFrames.Clear();

                    break;
                case StartNextPart startNextPart:
                    frameOffset = 0;
                    transform.position = carPathPair.Path.Entrance;
                    transform.rotation = Quaternion.Euler(0f, 0f, 90);
                    if (!isReplayMode)
                    {
                        isReplayMode = true;
                    }
                    break;
                case ResetPart resetPart:
                    frameOffset = 0;
                    transform.position = carPathPair.Path.Entrance;
                    transform.rotation = Quaternion.Euler(0f, 0f, 90);
                    if (!isReplayMode)
                    {
                        carPathPair.Path.UserInputPerFrames.Clear();
                    }

                    break;
                default:
                    break;
                case null:
                    throw new System.ArgumentNullException(nameof(aEvent));
            }
        }

        private void FixedUpdate()
        {
            frameOffset++;
            transform.Translate(carPathPair.Car.Speed * Time.deltaTime * 0.4f, 0, 0);

            if (isReplayMode)
            {
                carPathPair.Path.UserInputPerFrames.TryGetValue(frameOffset, out UserInputEvent userInputEvent);
                if (userInputEvent != null)
                {
                    Turn(userInputEvent);
                }
            }
        }

        public void SetReplayMode(bool aReplayMode)
        {
            isReplayMode = aReplayMode;
        }

        public bool IsReplayMode()
        {
            return isReplayMode;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {

            if (!isReplayMode && gameStatusController != null)
            {
                Debug.Log("OnTriggerEnter2D Tag:" + other.gameObject.tag);
                if (other.gameObject.tag == FINISH_TAG)
                {
                    gameStatusController.StartNextPart();
                }
                else if (other.gameObject.tag == CAR_TAG || other.gameObject.tag == OBSTACLE_TAG)
                {
                    gameStatusController.ResetPart();
                }
            }
        }
    }
}
