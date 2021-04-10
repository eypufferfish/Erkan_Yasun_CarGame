using UnityEngine;

namespace Mobge.CarGame.ErkanYasun.Data
{
    [CreateAssetMenu(fileName = "New Car", menuName = "Car Game/Components/Car", order = 51)]
    public class Car : Collideable
    {
        [SerializeField]
        private readonly HealthInfo healthInfo;
        [SerializeField]
        private readonly int speed;
    }
}