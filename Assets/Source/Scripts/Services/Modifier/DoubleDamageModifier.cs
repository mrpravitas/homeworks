public class DoubleDamageModifier : IGameModifier
{
    private IHealthService _healthService;
    private ILogger _logger;

    private float _previousMultiplier;

    public DoubleDamageModifier(IHealthService healthService, ILogger logger)
    {
        _healthService = healthService;
        _logger = logger;
    }

    public void OnEnterGameplay()
    {
        _previousMultiplier = _healthService.DamageMultiplier;
        _healthService.DamageMultiplier = _previousMultiplier * 2f;

        _logger.Log("Modifier activated: Double damage");
    }

    public void OnExitGameplay()
    {
        _healthService.DamageMultiplier = _previousMultiplier;
    }
}
