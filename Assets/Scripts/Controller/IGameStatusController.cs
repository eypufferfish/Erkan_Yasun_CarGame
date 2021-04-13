namespace Mobge.CarGame.ErkanYasun.Controller
{
    public interface IGameStatusController
    {
        void StartLevel();

        void StartNextPart();

        void ResetPart();


        void FinishPart();

        void FinishLevel();
    }
}
