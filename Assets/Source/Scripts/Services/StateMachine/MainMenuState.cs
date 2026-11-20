using UnityEngine;
using VContainer;

public class MainMenuState : IGameState
{
    private IMessageBus _bus;
    private IMainMenuView _mainMenuView;

    public MainMenuState(IMessageBus bus, IMainMenuView mainMenuView)
    {
        _bus = bus;
        _mainMenuView = mainMenuView;
    }

    public void Enter()
    {
        Time.timeScale = 0f;
        _mainMenuView.SetStartHandler(OnStartGame);
        _mainMenuView.SetExitHandler(OnExitGame);
    }

    public void Exit()
    {
        _mainMenuView.SetStartHandler(null);
        _mainMenuView.SetExitHandler(null);
    }

    private void OnStartGame()
    {
        _bus.Publish(new ChangeStateSignal(typeof(GameplayState)));
    }

    private void OnExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
