using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

public class PauseState : IGameState
{
    private IMessageBus _bus;
    private IPauseView _pauseView;

    public PauseState(IMessageBus bus, IPauseView pauseView)
    {
        _bus = bus;
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
        _bus.Publish(new ChangeStateSignal(typeof(GameplayState)));
    }

    private void OnExitToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
