using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyHealth _health;

    private EnemyPool _pool;

    private void Start()
    {
        _health.OnDied += Return;
    }

    public void SetPool(EnemyPool pool)
    {
        _pool = pool;
    }

    private void Return()
    {
        _health.Reset();
        gameObject.SetActive(false);
        _pool.Return(gameObject);
    }
}
