using Mobge.CarGame.ErkanYasun.Data.Event.UserInput;
using UnityEngine;

namespace Mobge.CarGame.ErkanYasun.Data
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
    }
}