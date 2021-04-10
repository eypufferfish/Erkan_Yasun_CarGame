using UnityEngine;
namespace Mobge.CarGame.ErkanYasun.Data
{
    [CreateAssetMenu(fileName = "NewObstacle", menuName = "Car Game/Components/Obstacle", order = 51)]
    public class Obstacle : Collideable
    {
        [SerializeField]
        private Bounds bounds;
    }
}
