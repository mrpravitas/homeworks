using System;
using UnityEngine;

public class HealthPickup :  MonoBehaviour, IEntityWithConfig
{
    public static event Action<string> OnEvent;

    private int _healthAmount;

    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();

        if (player != null )
        {
            OnEvent?.Invoke("Heal pickup collected");
            player.Heal(_healthAmount);
            Destroy(gameObject);
        }
    }

    public void Init(ScriptableObject config)
    {
        HealthPickupConfig healthPickupConfig = config as HealthPickupConfig;
        _healthAmount = healthPickupConfig.HealthAmount;
    }
}
