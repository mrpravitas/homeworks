using System;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public static event Action<string> OnEvent;

    [SerializeField] private int _healthAmount;

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
}
