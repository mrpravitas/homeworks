using System.Collections.Generic;

public class ProjectileSystem : ITickable
{
    private readonly List<Projectile> _projectiles = new();

    public void Register(Projectile projectile)
    {
        _projectiles.Add(projectile);
    }

    public void Tick(float deltaTime)
    {
        for (int i = _projectiles.Count - 1; i >= 0; i--)
        {
            Projectile projectile = _projectiles[i];

            if (projectile == null)
            {
                _projectiles.RemoveAt(i);
                continue;
            }

            projectile.Tick(deltaTime);
        }
    }
}
