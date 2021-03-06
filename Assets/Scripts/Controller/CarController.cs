using Mobge.CarGame.ErkanYasun.Model;
using Mobge.CarGame.ErkanYasun.Model.Event;
using Mobge.CarGame.ErkanYasun.Model.Event.GameStatus;
using Mobge.CarGame.ErkanYasun.Model.Event.UserInput;
using System;
using UnityEngine;

namespace Mobge.CarGame.ErkanYasun.Controller
{
    public class CarController : MonoBehaviour, ICarController
    {
        private const string FinishTag = "Finish";
        private const string CarTag = "Car";
        private const string ObstacleTag = "Obstacle";
        private const int RotationCoefficient = 30;
        private int frameOffset;

        [SerializeField]
        private CarPathPair carPathPair;

        [SerializeField]
        private bool isReplayMode;

        private GameStatusEvent currentGameStatus;

        private bool isPathCompleted;

        [SerializeField]
        private IGameStatusController gameStatusController;

        public UnityEngine.Camera cam;

        private void Start()
        {
            cam = Camera.main;
        }

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
            if (isReplayMode)
            {
                return;
            }

            Turn(aEvent);
            RecordEvent(aEvent);
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

        private void RecordEvent(BaseEvent aEvent)
        {
            carPathPair.Path.EventPerFrames.Add(frameOffset, aEvent);
        }

        private void TurnRight()
        {
            transform.Rotate(new Vector3(0, 0, -RotationCoefficient), Space.World);
        }

        private void TurnLeft()
        {
            transform.Rotate(new Vector3(0, 0, RotationCoefficient), Space.World);
        }

        public void HandleEvent(GameStatusEvent aEvent)
        {
            currentGameStatus = aEvent;
            switch (aEvent)
            {
                case StartLevel _:
                    frameOffset = 0;
                    transform.position = carPathPair.Path.Entrance;
                    transform.rotation = Quaternion.Euler(0f, 0f, 90);
                    carPathPair.Path.EventPerFrames.Clear();
                    break;

                case StartNextPart _:
                    frameOffset = 0;
                    transform.position = carPathPair.Path.Entrance;
                    transform.rotation = Quaternion.Euler(0f, 0f, 90);
                    isPathCompleted = false;
                    break;

                case ResetPart _:
                    frameOffset = 0;
                    transform.position = carPathPair.Path.Entrance;
                    transform.rotation = Quaternion.Euler(0f, 0f, 90);
                    if (!isReplayMode)
                    {
                        carPathPair.Path.EventPerFrames.Clear();
                    }
                    isPathCompleted = false;
                    break;

                case null:
                    throw new ArgumentNullException(nameof(aEvent));
            }
        }

        public void FixedUpdate()
        {
            if (isPathCompleted || currentGameStatus == null || currentGameStatus is FinishPart ||
                currentGameStatus is FinishLevel) return;
            frameOffset++;

            transform.Translate(carPathPair.Car.Speed * Time.deltaTime, 0, 0);
            if (CheckIfTransformOutOfScreenBorder())
            {
                gameStatusController.ResetPart();
                return;
            }

            if (isReplayMode)
            {
                carPathPair.Path.EventPerFrames.TryGetValue(frameOffset, out BaseEvent baseEvent);
                if (baseEvent != null)
                {
                    switch (baseEvent)
                    {
                        case UserInputEvent @event:
                            Turn(@event);
                            break;

                        case FinishPart _:
                            isPathCompleted = true;
                            break;
                    }
                }
            }
        }

        private bool CheckIfTransformOutOfScreenBorder()
        {
            Vector3 viewPos = cam.WorldToViewportPoint(transform.position);
            return viewPos.x < 0 || viewPos.x > 1 || viewPos.y < 0 || viewPos.y > 1 || viewPos.z < 0;
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
                                RecordEvent(ScriptableObject.CreateInstance<FinishPart>());
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