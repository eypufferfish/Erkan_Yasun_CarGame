using UnityEngine;
namespace Mobge.CarGame.ErkanYasun.Data
{
    [CreateAssetMenu(fileName = "NewLevel", menuName = "Car Game/Level", order = 51)]
    public class Level : ScriptableObject
    {
        [SerializeField]
        private readonly string levelName;
        [SerializeField]
        private readonly string shortDescription;
        [SerializeField]
        private readonly GameArea gameArea;

    }
}
