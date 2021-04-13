using Mobge.CarGame.ErkanYasun.Model;
using Mobge.CarGame.ErkanYasun.Model.Event.GameStatus;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mobge.CarGame.ErkanYasun.Controller
{
    public class LevelController : MonoBehaviour, IEventListener<GameStatusEvent>
    {
        [SerializeField]
        private Transform carPrefab;

        [SerializeField]
        private Transform startPrefab;

        [SerializeField]
        private Transform finishPreFab;

        [SerializeField]
        private Transform obstaclePreFab;

        [SerializeField]
        private Level levelData;

        public GameStatusController GameStatusController { get; set; }

        private List<CarPathPair>.Enumerator carPathEnumerator;

        private CarController currentActiveCarController;

        private UserInputController userInputController;
        private GameStatusEvent currentGameStatus;

        private void Awake()
        {
            GameStatusController = ScriptableObject.CreateInstance<GameStatusController>();
            GameStatusController.GameStatusDispatcher.RegisterListener(this);
            userInputController = gameObject.GetComponent(typeof(UserInputController)) as UserInputController;
            if (userInputController == null)
            {
                return;
            }

            GameStatusController.GameStatusDispatcher.RegisterListener(userInputController);
            userInputController.GameStatusController = GameStatusController;
        }

        // Start is called before the first frame update
        public void Start()
        {
            if (levelData == null)
            {
                return;
            }

            GameArea gameArea = levelData.GameArea;
            List<CarPathPair> carPathPairs = gameArea.CarPathPairs;
            if (carPathPairs != null)
            {
                carPathEnumerator = carPathPairs.GetEnumerator();
                if (carPathEnumerator.MoveNext())
                {
                    CarPathPair carPathPair = carPathEnumerator.Current;
                    if (carPathPair is { })
                    {
                        Path path = carPathPair.Path;
                        startPrefab = InstantiateStart(path.Entrance);
                        finishPreFab = InstantiateFinish(path.Target);
                        CreateCarPathComponents(carPathPair);
                    }
                }
            }

            InstantiateObstacles(gameArea.Obstacles);
        }

        private void InstantiateObstacles(SerializableDictionary<Vector2, Obstacle> obstacles)
        {
            if (obstacles == null)
            {
                return;
            }

            foreach (KeyValuePair<Vector2, Obstacle> obstaclePair in obstacles)
            {
                InstantiateObstacle(obstaclePair.Key, obstaclePair.Value);
            }
        }

        private void CreateCarPathComponents(CarPathPair carPathPair)
        {
            Car car = carPathPair.Car;
            Path path = carPathPair.Path;
            path.UserInputPerFrames.Clear();
            Transform carTransform = InstantiateCar(path.Entrance, car);
            currentActiveCarController = carTransform.gameObject.GetComponent(typeof(CarController)) as CarController;
            if (currentActiveCarController is { })
            {
                currentActiveCarController.SetReplayMode(false);
                currentActiveCarController.SetCarPathPair(carPathPair);
                currentActiveCarController.SetGameStatusController(GameStatusController);
                startPrefab.position = path.Entrance;
                finishPreFab.position = path.Target;
                userInputController.UserInputEventDispatcher.RegisterListener(currentActiveCarController);
                GameStatusController.GameStatusDispatcher.RegisterListener(currentActiveCarController);
                if (currentGameStatus != null)
                {
                    currentActiveCarController.HandleEvent(currentGameStatus);
                }
            }

            carTransform.gameObject.name = car.Sprite.name + "_" + Time.time;
        }

        private Transform InstantiateStart(Vector2 aEntrance)
        {
            return Instantiate(startPrefab, aEntrance, Quaternion.Euler(0f, 0f, 0));
        }

        private Transform InstantiateObstacle(Vector2 aPosition, Obstacle aObstacle)
        {
            obstaclePreFab.localScale = aObstacle.Size;
            return Instantiate(obstaclePreFab, aPosition, Quaternion.Euler(0f, 0f, 0));
        }

        private Transform InstantiateFinish(Vector2 aEntrance)
        {
            return Instantiate(finishPreFab, aEntrance, Quaternion.Euler(0f, 0f, 0));
        }

        private Transform InstantiateCar(Vector2 aEntrance, Car aCarData)
        {
            carPrefab.GetComponent<SpriteRenderer>().sprite = aCarData.Sprite;
            return Instantiate(carPrefab, aEntrance, Quaternion.Euler(0f, 0f, 90));
        }

        public void HandleEvent(GameStatusEvent aEvent)
        {
            currentGameStatus = aEvent;
            switch (aEvent)
            {
                case FinishPart _:
                    StartNextPart();
                    break;

                case null:
                    throw new ArgumentNullException(nameof(aEvent));
            }
        }

        private void StartNextPart()
        {
            if (carPathEnumerator.MoveNext())
            {
                CarPathPair carPathPair = carPathEnumerator.Current;
                CreateCarPathComponents(carPathPair);
                GameStatusController.StartNextPart();
            }
            else
            {
                GameStatusController.FinishLevel();
            }
        }
    }
}