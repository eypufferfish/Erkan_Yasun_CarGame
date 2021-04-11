using UnityEngine;

namespace Mobge.CarGame.ErkanYasun.Data
{
    public class HealthInfo : ScriptableObject
    {
        [SerializeField]
        private int initialHealth;
        [SerializeField]
        private int currentHealth;
    }
}