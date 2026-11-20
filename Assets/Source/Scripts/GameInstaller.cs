using UnityEngine;
using VContainer;

public class GameInstaller : MonoBehaviour
{
    private IMessageBus _bus;

    [Inject]
    public void Construct(IMessageBus bus)
    {
        _bus = bus;
        _bus.Publish(new GameStartedSignal());
    }
}
