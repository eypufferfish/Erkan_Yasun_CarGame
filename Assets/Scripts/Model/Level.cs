using UnityEngine;
namespace Mobge.CarGame.ErkanYasun.Model
{
    [CreateAssetMenu(fileName = "NewLevel", menuName = "Car Game/Level", order = 51)]
    public class Level : ScriptableObject
    {
        [SerializeField]
        private string levelName;
        [SerializeField]
        private string shortDescription;
        [SerializeField]
        [Header("[1..10]")]
        private int difficulty;
        [SerializeField]
        private GameArea gameArea;

    }
}
