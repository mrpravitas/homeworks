using UnityEngine.SceneManagement;

public class GameOverState : IGameState
{
    private IGameOverView _gameOverView;

    public GameOverState(IGameOverView gameOverView)
    {
        _gameOverView = gameOverView;
    }

    public void Enter()
    {
        _gameOverView.SetRestartHandler(HandleRestart);
    }

    public void Exit()
    {
        _gameOverView.SetRestartHandler(null);
    }

    private void HandleRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
