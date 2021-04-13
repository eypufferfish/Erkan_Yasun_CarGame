using Mobge.CarGame.ErkanYasun.Model.Event.UserInput;
using UnityEngine;

namespace Mobge.CarGame.ErkanYasun.Model
{
    [CreateAssetMenu(fileName = "New Path", menuName = "Car Game/Components/Path", order = 51)]
    public class Path : ScriptableObject
    {
        [SerializeField]
        [Header("x Ekseni [-8..8], y Ekseni [-4..4]")]
        private Vector2 entrance;

        [SerializeField]
        [Header("x Ekseni [-8..8], y Ekseni [-4..4]")]
        private Vector2 target;

        [SerializeField]
        private readonly SerializableDictionary<int, UserInputEvent> userInputPerFrames = new SerializableDictionary<int, UserInputEvent>();

        public SerializableDictionary<int, UserInputEvent> UserInputPerFrames => userInputPerFrames;

        public Vector2 Entrance => entrance;
        public Vector2 Target => target;
    }
}