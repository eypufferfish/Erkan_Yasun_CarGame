using UnityEngine;

namespace Mobge.CarGame.ErkanYasun.Model
{
    [CreateAssetMenu(fileName = "New CarPathPair", menuName = "Car Game/Components/CarPathPair", order = 51)]
    public class CarPathPair : ScriptableObject
    {
        [SerializeField]
        private Car car;
        [SerializeField]
        private Path path;

        public Car Car => car;
        public Path Path => path;

    }
}