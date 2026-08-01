using UnityEngine;
using UnityEngine.UI;

public class GameInstaller : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private Button _switchInputButton;
    [SerializeField] private UITextHealthPresenter _healthPresenter;

    private IInputService _keyboardInput;
    private IInputService _mouseInput;
    private IInputService _currentInput;

    private IMovementService _movementService;

    private IHealthService _healthService;

    private ILogger _logger;

    private void Awake()
    {
        CreateServices();
        InitGame();
    }

    private void OnEnable()
    {
        _switchInputButton.onClick.AddListener(SwitchInput);
    }

    private void OnDisable()
    {
        _switchInputButton.onClick.RemoveListener(SwitchInput);
        _logger.Dispose();
    }

    private void CreateServices()
    {
        _keyboardInput = new KeyboardInputService();
        _mouseInput = new MouseInputService();
        _currentInput = _keyboardInput;

        TransformMovementService movementService = new TransformMovementService();
        movementService.Init(_player.transform);
        _movementService = movementService;

        _logger = new ConsoleLogger();

        _healthService = new HealthService(_logger, _healthPresenter);
    }

    private void InitGame()
    {
        _logger.Init();
        _player.Init(_currentInput, _movementService, _healthService);

        _logger.Log("Game has started");
    }

    private void SwitchInput()
    {
        _currentInput = _currentInput == _keyboardInput 
            ? _mouseInput 
            : _keyboardInput;

        _player.SetInputService(_currentInput);

        _logger.Log("Input method switched");
    }
}
