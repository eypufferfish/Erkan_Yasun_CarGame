using Mobge.CarGame.ErkanYasun.Model;
using System.Collections.Generic;
using UnityEngine;

namespace Mobge.CarGame.ErkanYasun.Controller
{
    public class LevelController : MonoBehaviour
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

        private void Awake()
        {
            GameStatusController = ScriptableObject.CreateInstance<GameStatusController>();
        }

        // Start is called before the first frame update
        private void Start()
        {
            UserInputController userInputController = gameObject.GetComponent(typeof(UserInputController)) as UserInputController;

            if (levelData != null)
            {
                GameArea gameArea = levelData.GameArea;
                List<CarPathPair> carPathPairs = gameArea.CarPathPairs;
                if (carPathPairs != null)
                {
                    foreach (CarPathPair carPathPair in carPathPairs)
                    {
                        Car car = carPathPair.Car;
                        Path path = carPathPair.Path;
                        Transform carTransform = InstantiateCar(path.Entrance, car);
                        CarController carController = carTransform.gameObject.GetComponent(typeof(CarController)) as CarController;
                        carController.SetCarPathPair(carPathPair);
                        carController.SetGameStatusController(GameStatusController);
                        GameStatusController.GameStatusDispatcher.RegisterListener(carController);
                        userInputController.UserInputEventDispatcher.RegisterListener(carController);
                        InstantiateStart(path.Entrance);
                        InstantiateFinish(path.Target);
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
    }
}
