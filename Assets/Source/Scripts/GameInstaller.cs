using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class GameInstaller : MonoBehaviour
{
    [Header("Configs")]
    [SerializeField] private GameplayConfig _gameplayConfig;

    [Header("UI")]
    [SerializeField] private MainMenuView _mainMenuView;
    [SerializeField] private GameplayInputView _gameplayInputView;
    [SerializeField] private GameplayHud _hud;
    [SerializeField] private PauseView _pauseView;
    [SerializeField] private GameOverView _gameOverView;

    [Header("Modifiers")]
    [SerializeField] private bool _enableDoubleDamage;
    [SerializeField] private bool _enableHighSpeed;
    [SerializeField] private bool _enabledHighSpawn;

    private IMovementService _movementService;
    private IHealthService _healthService;
    private ILogger _logger;
    private IMessageBus _bus;

    private GameStateMachine _stateMachine;

    private IReadOnlyList<IEntityFactory> _factories;

    private List<IGameModifier> _gameModifiers;

    [Inject]
    public void Construct(
        ILogger logger,
        IMessageBus bus,
        IMovementService movementService,
        IHealthService healthService,
        IReadOnlyList<IEntityFactory> factories)
    {
        _logger = logger;
        _bus = bus;
        _movementService = movementService;
        _healthService = healthService;
        _factories = factories;

        _bus.Publish(new GameStartedSignal());
        InitStates();
    }

    private void Update()
    {
        _stateMachine.Tick(Time.deltaTime);
    }

    private void InitStates()
    {
        _stateMachine = new GameStateMachine();
        _stateMachine.Init(_logger, _healthService);

        _stateMachine.Register(new MainMenuState(_stateMachine, _mainMenuView));

        CreateModifiers();
        _stateMachine.Register(new GameplayState(_stateMachine, _gameplayInputView, 
            _gameplayConfig, _factories, _gameModifiers, _hud));

        _stateMachine.Register(new PauseState(_stateMachine, _pauseView));
        _stateMachine.Register(new GameOverState(_stateMachine, _gameOverView));

        _stateMachine.ChangeState<MainMenuState>();
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
        if (_enabledHighSpawn)
        {
            _gameModifiers.Add(new HighSpawnMultiplier(_factories, _logger));
        }
    }
}
