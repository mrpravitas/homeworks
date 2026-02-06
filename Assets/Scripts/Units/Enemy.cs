using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void OnMouseDown()
    {
        GameEvent gameEvent = new GameEvent(
            EventType.EnemyDefeated,
            Time.time,
            "enemy was defeated",
            (Vector2)transform.position);

        EventManager.TriggerEvent(gameEvent);

        Destroy(gameObject);
    }
}
