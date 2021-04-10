using UnityEngine;

namespace Mobge.CarGame.ErkanYasun.Data
{
    [CreateAssetMenu(fileName = "New CarPathPair", menuName = "Car Game/Components/CarPathPair", order = 51)]
    public class CarPathPair : ScriptableObject
    {
        [SerializeField]
        private readonly Car car;
        [SerializeField]
        private readonly Path path;

        public Path Path => path;
    }
}