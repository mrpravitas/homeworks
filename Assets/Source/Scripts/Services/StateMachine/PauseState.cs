using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseState : IGameState
{
    private GameStateMachine _stateMashine;
    private IPauseView _pauseView;

    public PauseState(GameStateMachine stateMashine, IPauseView pauseView)
    {
        _stateMashine = stateMashine;
        _pauseView = pauseView;
    }

    public void Enter()
    {
        Time.timeScale = 0f;

        _pauseView.SetResumeHandler(OnResume);
        _pauseView.SetExitToMenuHandler(OnExitToMenu);
    }

    public void Exit()
    {
        Time.timeScale = 1f;

        _pauseView.SetResumeHandler(null);
        _pauseView.SetExitToMenuHandler(null);
    }

    private void OnResume()
    {
        _stateMashine.ChangeState<GameplayState>();
    }

    private void OnExitToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}