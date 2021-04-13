namespace Mobge.CarGame.ErkanYasun.Controller
{
    public interface IEventListener<in T>
    {
        void HandleEvent(T aEvent);
    }
}