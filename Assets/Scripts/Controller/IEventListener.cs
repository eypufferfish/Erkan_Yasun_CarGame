namespace Mobge.CarGame.ErkanYasun.Controller
{
    public interface IEventListener<T>
    {
        void HandleEvent(T aEvent);
    }
}
