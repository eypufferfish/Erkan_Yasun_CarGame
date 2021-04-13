using Mobge.CarGame.ErkanYasun.Model;
using Mobge.CarGame.ErkanYasun.Model.Event.GameStatus;
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



        private void Awake()
        {
            GameStatusController = ScriptableObject.CreateInstance<GameStatusController>();
            GameStatusController.GameStatusDispatcher.RegisterListener(this);
            userInputController = gameObject.GetComponent(typeof(UserInputController)) as UserInputController;
        }

        // Start is called before the first frame update
        private void Start()
        {


            if (levelData != null)
            {
                GameArea gameArea = levelData.GameArea;
                List<CarPathPair> carPathPairs = gameArea.CarPathPairs;
                if (carPathPairs != null)
                {

                    carPathEnumerator = carPathPairs.GetEnumerator();
                    if (carPathEnumerator.MoveNext())
                    {
                        CarPathPair carPathPair = carPathEnumerator.Current;
                        Car car = carPathPair.Car;
                        Path path = carPathPair.Path;
                        startPrefab = InstantiateStart(path.Entrance);
                        finishPreFab = InstantiateFinish(path.Target);

                        CreateCarPathComponents(carPathPair);
                    }
                }

                SerializableDictionary<Vector2, Obstacle> obstacles = gameArea.Obstacles;
                if (obstacles != null)
                {
                    foreach (KeyValuePair<Vector2, Obstacle> obstaclePair in obstacles)
                    {
                        InstantiateObstacle(obstaclePair.Key, obstaclePair.Value);
                    }
                }

            }

        }

        private void CreateCarPathComponents(CarPathPair carPathPair)
        {
            Car car = carPathPair.Car;
            Path path = carPathPair.Path;
            path.UserInputPerFrames.Clear();
            Transform carTransform = InstantiateCar(path.Entrance, car);
            currentActiveCarController = carTransform.gameObject.GetComponent(typeof(CarController)) as CarController;
            currentActiveCarController.SetReplayMode(false);
            currentActiveCarController.SetCarPathPair(carPathPair);
            currentActiveCarController.SetGameStatusController(GameStatusController);
            startPrefab.position = path.Entrance;
            finishPreFab.position = path.Target;
            userInputController.UserInputEventDispatcher.RegisterListener(currentActiveCarController);
            GameStatusController.GameStatusDispatcher.RegisterListener(currentActiveCarController);
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

        // Update is called once per frame
        private void Update()
        {

        }

        public void HandleEvent(GameStatusEvent aEvent)
        {
            Debug.Log("Level Controller Handle Game Status Event:" + aEvent);
            switch (aEvent)
            {
                case StartLevel startLevel:
                    StartLevel();
                    break;
                case StartNextPart startNextPart:
                    if (carPathEnumerator.MoveNext())
                    {
                        CarPathPair carPathPair = carPathEnumerator.Current;
                        CreateCarPathComponents(carPathPair);
                    }
                    break;
                case ResetPart resetPart:

                    break;
                default:
                    break;
                case null:
                    throw new System.ArgumentNullException(nameof(aEvent));
            }
        }

        private void StartLevel()
        {
            if (levelData != null)
            {
                GameArea gameArea = levelData.GameArea;
                List<CarPathPair> carPathPairs = gameArea.CarPathPairs;
                carPathEnumerator = carPathPairs.GetEnumerator();
                if (carPathEnumerator.MoveNext())
                {

                }

            }
        }
    }
}
