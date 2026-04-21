using UnityEngine;

public class Player : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy enemy))
        {
            EventBus.Raise(new GameEvent(EventType.GameLosed));
        }
    }
}
