using UnityEngine;

public class GameInstaller : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private SwitchInputButton _switchInputCommand;
    [SerializeField] private UITextHealthPresenter _healthPresenter;

    [SerializeField] private MainMenuView _mainMenuView;
    [SerializeField] private GameplayInputView _gameplayInputView;
    [SerializeField] private PauseView _pauseView;

    private IInputService _keyboardInput;
    private IInputService _mouseInput;
    private IInputService _currentInput;

    private IMovementService _movementService;

    private IHealthService _healthService;

    private ILogger _logger;

    private GameStateMachine _stateMachine;

    private void Awake()
    {
        CreateServices();
        InitGame();
        InitStates();
    }

    private void OnDisable()
    {
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
        _switchInputCommand.SetHandler(SwitchInput);

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

    private void InitStates()
    {
        _stateMachine = new GameStateMachine();

        _stateMachine.Register(new MainMenuState(_stateMachine, _mainMenuView));
        _stateMachine.Register(new GameplayState(_stateMachine, _gameplayInputView));
        _stateMachine.Register(new PauseState(_stateMachine, _pauseView));
        _stateMachine.Register(new GameOverState(_stateMachine));

        _stateMachine.ChangeState<MainMenuState>();
    }
}
