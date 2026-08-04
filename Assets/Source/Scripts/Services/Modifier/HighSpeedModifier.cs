public class HighSpeedModifier : IGameModifier
{
    private IMovementService _movementService;
    private ILogger _logger;

    private float _previousMultiplier;

    public HighSpeedModifier(IMovementService movementService, ILogger logger)
    {
        _movementService = movementService;
        _logger = logger;
    }

    public void OnEnterGameplay()
    {
        _previousMultiplier = _movementService.SpeedMultiplier;
        _movementService.SpeedMultiplier = _previousMultiplier * 1.5f;

        _logger.Log("Modifier activated: High speed");
    }

    public void OnExitGameplay()
    {
        _movementService.SpeedMultiplier = _previousMultiplier;
    }
}
