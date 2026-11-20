using UnityEngine;
using VContainer;

public class HealthPickup : MonoBehaviour, IEntityWithConfig
{
    private IMessageBus _bus;
    private int _healthAmount;

    [Inject]
    public void Construct(IMessageBus bus)
    {
        _bus = bus;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() == null)
        {
            return;
        }

        _bus.Publish(new HealthPickupCollectedSignal(_healthAmount));
        Destroy(gameObject);
    }

    public void Init(ScriptableObject config)
    {
        HealthPickupConfig healthPickupConfig = config as HealthPickupConfig;
        _healthAmount = healthPickupConfig.HealthAmount;
    }
}
