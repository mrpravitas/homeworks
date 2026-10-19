using UnityEngine;

public class EnemyGoal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            enemy.Kill();
        }
    }
}
