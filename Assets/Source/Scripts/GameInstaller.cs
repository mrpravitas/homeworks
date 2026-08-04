using System.Collections.Generic;
using UnityEngine;

public class GameInstaller : MonoBehaviour
{
    [Header("Configs")]
    [SerializeField] private HealthConfig healthConfig;
    [SerializeField] private MovementConfig movementConfig;
    [SerializeField] private GameplayConfig _gameplayConfig;

    [Header("Entity")]
    [SerializeField] private EntitySpawnEntry[] _spawnEntries;

    [Header("Player")]
    [SerializeField] private PlayerController _player;

    [Header("UI")]
    [SerializeField] private SwitchInputButton _switchInputCommand;
    [SerializeField] private UITextHealthPresenter _healthPresenter;
    [SerializeField] private MainMenuView _mainMenuView;
    [SerializeField] private GameplayInputView _gameplayInputView;
    [SerializeField] private PauseView _pauseView;
    [SerializeField] private GameOverView _gameOverView;

    [Header("Modifiers")]
    [SerializeField] private bool _enableDoubleDamage;
    [SerializeField] private bool _enableHighSpeed;

    private IInputService _keyboardInput;
    private IInputService _mouseInput;
    private IInputService _currentInput;

    private IMovementService _movementService;
    private IHealthService _healthService;
    private ILogger _logger;

    private GameStateMachine _stateMachine;

    private IEntityFactory<Object>[] _factories;

    private List<IGameModifier> _gameModifiers;

    private void Awake()
    {
        CreateServices();
        CreateFactories();
        InitGame();
        InitStates();
    }

    private void Update()
    {
        _stateMachine.Tick(Time.deltaTime);
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
        movementService.Init(movementConfig, _player.transform);
        _movementService = movementService;

        _logger = new ConsoleLogger();

        _healthService = new HealthService(healthConfig, _logger, _healthPresenter);
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
        _stateMachine.Init(_logger, _healthService);

        _stateMachine.Register(new MainMenuState(_stateMachine, _mainMenuView));

        CreateModifiers();
        _stateMachine.Register(new GameplayState(_stateMachine, _gameplayInputView, 
            _gameplayConfig, _factories, _gameModifiers));

        _stateMachine.Register(new PauseState(_stateMachine, _pauseView));
        _stateMachine.Register(new GameOverState(_stateMachine, _gameOverView));

        _stateMachine.ChangeState<MainMenuState>();
    }

    private void CreateFactories()
    {
        _factories = new IEntityFactory<Object>[_spawnEntries.Length];

        for (int i = 0; i < _factories.Length; i++)
        {
            var entry = _spawnEntries[i];

            _factories[i] = new EntityFactory<Object>(
                entry.Prefab,
                entry.Config,
                entry.SpawnChance);
        }
    }

    private void CreateModifiers()
    {
        _gameModifiers = new List<IGameModifier>();

        if (_enableDoubleDamage)
        {
            _gameModifiers.Add(new DoubleDamageModifier(_healthService, _logger));
        }
        if (_enableHighSpeed)
        {
            _gameModifiers.Add(new HighSpeedModifier(_movementService, _logger));
        }
    }
}
