public class PlayerHealth : Health
{
    protected override void Die()
    {
        Destroy(gameObject);
    }
}
