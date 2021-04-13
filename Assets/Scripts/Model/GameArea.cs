using System.Collections.Generic;
using UnityEngine;

namespace Mobge.CarGame.ErkanYasun.Model
{
    [CreateAssetMenu(fileName = "NewGameArea", menuName = "Car Game/Components/Game Area", order = 51)]
    public class GameArea : ScriptableObject
    {
        [SerializeField]
        private SerializableDictionary<Vector2, Obstacle> obstacles = new SerializableDictionary<Vector2, Obstacle>();

        [SerializeField]
        private List<CarPathPair> carPathPairs;

        private int activeCarPathIndex;

        public List<CarPathPair> CarPathPairs => carPathPairs;
        public SerializableDictionary<Vector2, Obstacle> Obstacles => obstacles;
    }
}