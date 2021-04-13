using System;
using Mobge.CarGame.ErkanYasun.Model;
using Mobge.CarGame.ErkanYasun.Model.Event.GameStatus;
using Mobge.CarGame.ErkanYasun.Model.Event.UserInput;
using UnityEngine;

namespace Mobge.CarGame.ErkanYasun.Controller
{
    public class CarController : MonoBehaviour, ICarController
    {
        private const string FinishTag = "Finish";
        private const string CarTag = "Car";
        private const string ObstacleTag = "Obstacle";
        private int frameOffset;

        [SerializeField]
        private CarPathPair carPathPair;

        [SerializeField]
        private bool isReplayMode;

        private GameStatusEvent currentGameStatus;

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
            if (isReplayMode) return;
            Turn(aEvent);
            RecordUserInput(aEvent);
        }

        private void Turn(UserInputEvent aEvent)
        {
            switch (aEvent)
            {
                case TurnLeft _:
                    TurnLeft();
                    break;

                case TurnRight _:
                    TurnRight();
                    break;

                case null:
                    throw new ArgumentNullException(nameof(aEvent));
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
            currentGameStatus = aEvent;
            Debug.Log(gameObject.name + " Handle Game Status Event:" + aEvent);
            switch (aEvent)
            {
                case StartLevel _:
                    frameOffset = 0;
                    transform.position = carPathPair.Path.Entrance;
                    transform.rotation = Quaternion.Euler(0f, 0f, 90);
                    carPathPair.Path.UserInputPerFrames.Clear();

                    break;

                case StartNextPart _:
                    frameOffset = 0;
                    transform.position = carPathPair.Path.Entrance;
                    transform.rotation = Quaternion.Euler(0f, 0f, 90);
                    if (!isReplayMode)
                    {
                        isReplayMode = true;
                    }
                    break;

                case ResetPart _:
                    frameOffset = 0;
                    transform.position = carPathPair.Path.Entrance;
                    transform.rotation = Quaternion.Euler(0f, 0f, 90);
                    if (!isReplayMode)
                    {
                        carPathPair.Path.UserInputPerFrames.Clear();
                    }

                    break;

                case null:
                    throw new ArgumentNullException(nameof(aEvent));
            }
        }

        private void FixedUpdate()
        {
            if (!(currentGameStatus == null || currentGameStatus is FinishPart || currentGameStatus is FinishLevel))
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
        }

        public void SetReplayMode(bool aReplayMode)
        {
            isReplayMode = aReplayMode;
        }

        public bool IsReplayMode()
        {
            return isReplayMode;
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            switch (isReplayMode)
            {
                case false when gameStatusController != null:
                    {
                        switch (other.gameObject.tag)
                        {
                            case FinishTag:
                                gameStatusController.FinishPart();
                                break;

                            case CarTag:
                            case ObstacleTag:
                                gameStatusController.ResetPart();
                                break;
                        }

                        break;
                    }
            }
        }
    }
}