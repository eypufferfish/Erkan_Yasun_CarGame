namespace Mobge.CarGame.ErkanYasun.Controller
{
    public interface IGameStatusController
    {
        void StartLevel();

        void StartNextPart();

        void FinishPart();
        void ResetPart();

        void FinishLevel();
    }
}
