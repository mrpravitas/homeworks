using UnityEngine;
using VContainer;

public class PlayerController : MonoBehaviour
{
    private IInputService _inputService;
    private IMovementService _movementService;

    [Inject]
    public void Construct(IInputService inputService, IMovementService movementService)
    {
        _inputService = inputService;
        _movementService = movementService;
    }

    private void Update()
    {
        Vector2 direction = _inputService.GetMovementDirection();
        _movementService.Move(direction);
    }
}
