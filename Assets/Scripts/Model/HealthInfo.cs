using UnityEngine;

namespace Mobge.CarGame.ErkanYasun.Model
{
    [CreateAssetMenu(menuName = "Car Game/Components/Health Info", order = 51)]
    public class HealthInfo : ScriptableObject
    {
        [SerializeField]
        private int initialHealth;

        [SerializeField]
        private int currentHealth;
    }
}