using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private int _healthAmount;

    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();

        if (player != null )
        {
            player.Heal(_healthAmount);
            Destroy(gameObject);
        }
    }
}
