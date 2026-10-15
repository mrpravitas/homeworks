using System.Collections.Generic;

public class EnemySystem : ITickable
{
    private readonly List<Enemy> _enemies = new();

    public void Register(Enemy enemy)
    {
        _enemies.Add(enemy);
    }

    public void Tick(float deltaTime)
    {
        for (int i = _enemies.Count - 1; i >= 0; i--)
        {
            Enemy enemy = _enemies[i];

            if (enemy == null)
            {
                _enemies.RemoveAt(i);
                continue;
            }

            enemy.Tick(deltaTime);
        }
    }
}
