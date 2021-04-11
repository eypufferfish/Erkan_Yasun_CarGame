using UnityEngine;
namespace Mobge.CarGame.ErkanYasun.Data
{
    [CreateAssetMenu(fileName = "NewLevel", menuName = "Car Game/Level", order = 51)]
    public class Level : ScriptableObject
    {
        [SerializeField]
        private string levelName;
        [SerializeField]
        private  string shortDescription;
        [SerializeField]
        private  GameArea gameArea;

    }
}
