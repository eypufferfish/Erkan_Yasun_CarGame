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

        public void Awake()
        {
            startLevel = CreateInstance<StartLevel>();
            finishLevel = CreateInstance<FinishLevel>();
            startNextPart = CreateInstance<StartNextPart>();
            finishPart = CreateInstance<FinishPart>();
            resetPart = CreateInstance<ResetPart>();
            GameStatusDispatcher = CreateInstance<GameStatusDispatcher>();
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

        public void FinishPart()
        {
            GameStatusDispatcher.DispatchEvent(finishPart);
        }

        public void ResetPart()
        {
            GameStatusDispatcher.DispatchEvent(resetPart);
        }
    }
}