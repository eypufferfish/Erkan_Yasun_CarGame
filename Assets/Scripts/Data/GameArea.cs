using System.Collections.Generic;
using UnityEngine;
namespace Mobge.CarGame.ErkanYasun.Data
{
    [CreateAssetMenu(fileName = "NewGameArea", menuName = "Car Game/Components/Game Area", order = 51)]
    public class GameArea : ScriptableObject
    {
        [SerializeField]
        private readonly List<Obstacle> obstacles;
        [SerializeField]
        private readonly List<CarPathPair> carPathPairs;
        private readonly int activeCarPathIndex;

    }
}
