using Mobge.CarGame.ErkanYasun.Model.Event.GameStatus;
using UnityEngine;


namespace Mobge.CarGame.ErkanYasun.Controller
{
    public class GameStatusController : ScriptableObject, IGameStatusController
    {
        private GameStatusEvent startLevel;
        private GameStatusEvent finishLevel;
        private GameStatusEvent startNextPart;
        private GameStatusEvent finishPart;
        private GameStatusEvent resetPart;

        public GameStatusDispatcher GameStatusDispatcher { get; set; }

        private void Awake()
        {
            startLevel = ScriptableObject.CreateInstance<StartLevel>();
            finishLevel = ScriptableObject.CreateInstance<FinishLevel>();
            startNextPart = ScriptableObject.CreateInstance<StartNextPart>();
            finishPart = ScriptableObject.CreateInstance<FinishPart>();
            resetPart = ScriptableObject.CreateInstance<ResetPart>();
            GameStatusDispatcher = ScriptableObject.CreateInstance<GameStatusDispatcher>();

        }

        public void StartLevel()
        {
            GameStatusDispatcher.DispatchEvent(startLevel);
        }

        public void FinishLevel()
        {
            GameStatusDispatcher.DispatchEvent(finishLevel);
        }

        public void StartNextPart()
        {
            GameStatusDispatcher.DispatchEvent(startNextPart);
        }

        public void ResetPart()
        {
            GameStatusDispatcher.DispatchEvent(resetPart);
        }


        public void FinishPart()
        {
            GameStatusDispatcher.DispatchEvent(finishPart);
        }

    }
}
