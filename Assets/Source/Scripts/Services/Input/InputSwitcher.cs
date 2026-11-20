using UnityEngine;

public sealed class InputSwitcher : IInputSwitcher
{
    private readonly IInputService _keyboard;
    private readonly IInputService _mouse;
    private readonly IMessageBus _bus;
    private IInputService _current;

    public InputSwitcher(KeyboardInputService keyboard, MouseInputService mouse, IMessageBus bus)
    {
        _keyboard = keyboard;
        _mouse = mouse;
        _bus = bus;
        _current = keyboard;
    }

    public Vector2 GetMovementDirection() => _current.GetMovementDirection();

    public void Switch()
    {
        _current = _current == _keyboard ? _mouse : _keyboard;

        string name = _current == _keyboard
            ? nameof(KeyboardInputService)
            : nameof(MouseInputService);

        _bus.Publish(new InputSwitchedSignal(name));
    }
}
