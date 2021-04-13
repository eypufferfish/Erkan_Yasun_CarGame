using Mobge.CarGame.ErkanYasun.Model.Event.GameStatus;
using Mobge.CarGame.ErkanYasun.Model.Event.UserInput;

namespace Mobge.CarGame.ErkanYasun.Controller
{
    public interface ICarController : IEventListener<UserInputEvent>, IEventListener<GameStatusEvent>
    {

        void SetReplayMode(bool aReplayMode);

        bool IsReplayMode();
    }
}
