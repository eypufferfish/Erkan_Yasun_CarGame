using Mobge.CarGame.ErkanYasun.Model.Event.UserInput;
using UnityEngine;

namespace Mobge.CarGame.ErkanYasun.Model
{
    [CreateAssetMenu(fileName = "New Path", menuName = "Car Game/Components/Path", order = 51)]
    public class Path : ScriptableObject
    {
        [SerializeField]
        private Vector2 entrance;
        [SerializeField]
        private Vector2 target;
        [SerializeField]
        private SerializableDictionary<int, UserInputEvent> userInputPerFrames = new SerializableDictionary<int, UserInputEvent>();

        public SerializableDictionary<int, UserInputEvent> UserInputPerFrames => userInputPerFrames;

        public Vector2 Entrance => entrance;
        public Vector2 Target => target;
    }
}