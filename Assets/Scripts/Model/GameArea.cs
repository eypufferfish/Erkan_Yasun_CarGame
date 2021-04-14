using System.Collections.Generic;
using UnityEngine;

namespace Mobge.CarGame.ErkanYasun.Model
{
    [CreateAssetMenu(fileName = "NewGameArea", menuName = "Car Game/Components/Game Area", order = 51)]
    public class GameArea : ScriptableObject
    {
        [SerializeField]
        [Header("x Ekseni [-8..8], y Ekseni [-4..4]")]
        private SerializableDictionary<Vector2, Obstacle> obstacles = new SerializableDictionary<Vector2, Obstacle>();

        [SerializeField]
        private List<CarPathPair> carPathPairs;

        public List<CarPathPair> CarPathPairs => carPathPairs;
        public SerializableDictionary<Vector2, Obstacle> Obstacles => obstacles;
    }
}