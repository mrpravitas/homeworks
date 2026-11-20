using UnityEngine;
using VContainer;

public class PlayerController : MonoBehaviour
{
    private IInputService _inputService;
    private IMovementService _movementService;
    private IHealthService _healthService;

    [Inject]
    public void Construct(
        IInputService inputService,
        IMovementService movementService,
        IHealthService healthService)
    {
        _inputService = inputService;
        _movementService = movementService;
        _healthService = healthService;
    }

    private void Update()
    {
        Vector2 direction = _inputService.GetMovementDirection();
        _movementService.Move(direction);
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
