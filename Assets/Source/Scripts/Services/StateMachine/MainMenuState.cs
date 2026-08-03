using UnityEngine;

public class MainMenuState : IGameState
{
    private GameStateMachine _stateMashine;
    private IMainMenuView _mainMenuView;

    public MainMenuState(GameStateMachine stateMashine, IMainMenuView mainMenuView)
    {
        _stateMashine = stateMashine;
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
        _stateMashine.ChangeState<GameplayState>();
    }

    private void OnExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
