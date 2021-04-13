using UnityEngine;

namespace Mobge.CarGame.ErkanYasun.Model
{
    [CreateAssetMenu(fileName = "NewObstacle", menuName = "Car Game/Components/Obstacle", order = 51)]
    public class Obstacle : Collideable
    {
        [SerializeField]
        private Vector2 size;

        public Vector2 Size => size;
    }
}