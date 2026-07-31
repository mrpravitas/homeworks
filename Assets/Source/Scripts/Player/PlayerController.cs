using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private IInputService _inputService;
    private IMovementService _movementService;
    private IHealthService _healthService;

    private void Update()
    {
        Vector2 direction = _inputService.GetMovementDirection();
        _movementService.Move(direction);
    }

    public void Init(IInputService inputService, IMovementService movementService, IHealthService health)
    {
        _inputService = inputService;
        _movementService = movementService;
        _healthService = health;
    }

    public void SetInputService(IInputService inputService)
    {
        _inputService = inputService;
    }

    public void SetMovementService(IMovementService movementService)
    {
        _movementService = movementService;
    }

    public void SetHealthService(IHealthService healthService)
    {
        _healthService = healthService;
    }

    public void TakeDamage(int amount)
    {
        _healthService.TakeDamage(amount);
    }

    public void Heal(int amount)
    {
        _healthService.Heal(amount);
    }
}
