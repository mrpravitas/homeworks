using UnityEngine;

public class Player : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent<Enemy>(out Enemy enemy))
        {
            EventBus.Raise(new GameEvent(EventType.PlayerDamaged));
        }
    }
}
