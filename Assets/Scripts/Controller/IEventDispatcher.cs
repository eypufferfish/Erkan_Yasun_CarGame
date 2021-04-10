namespace Mobge.CarGame.ErkanYasun.Controller
{
    public interface IEventDispatcher<T>
    {
        void RegisterListener(IEventListener<T> aListener);

        void UnRegisterListener(IEventListener<T> aListener);

        void DispatchEvent(T aEvent);

    }
}
