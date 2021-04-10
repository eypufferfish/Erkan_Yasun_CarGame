using UnityEngine;

namespace Mobge.CarGame.ErkanYasun.Data
{
    public class HealthInfo : ScriptableObject
    {
        [SerializeField]
        private readonly int initialHealth;
        [SerializeField]
        private readonly int currentHealth;
    }
}