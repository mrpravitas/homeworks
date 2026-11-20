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

    private IHealthService _healthService;
    private ILogger _logger;
    private IMessageBus _bus;

    private GameStateMachine _stateMachine;

    private IReadOnlyList<IEntityFactory> _factories;
    private IReadOnlyList<IGameModifier> _gameModifiers;

    [Inject]
    public void Construct(
        ILogger logger,
        IMessageBus bus,
        IHealthService healthService,
        IReadOnlyList<IEntityFactory> factories,
        IReadOnlyList<IGameModifier> modifiers)
    {
        _logger = logger;
        _bus = bus;
        _healthService = healthService;
        _factories = factories;
        _gameModifiers = modifiers;

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
        _stateMachine.Register(new GameplayState(_stateMachine, _gameplayInputView,
            _gameplayConfig, _factories, _gameModifiers, _hud));
        _stateMachine.Register(new PauseState(_stateMachine, _pauseView));
        _stateMachine.Register(new GameOverState(_stateMachine, _gameOverView));

        _stateMachine.ChangeState<MainMenuState>();
    }
}
