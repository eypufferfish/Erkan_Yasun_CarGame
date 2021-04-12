using UnityEngine;

namespace Mobge.CarGame.ErkanYasun.Model
{
    [CreateAssetMenu(fileName = "New Car", menuName = "Car Game/Components/Car", order = 51)]
    public class Car : Collideable
    {
        [SerializeField]
        private HealthInfo healthInfo;
        [SerializeField]
        private int speed;
        [SerializeField]
        private Sprite sprite;
    }
}