using UnityEngine.SceneManagement;

public class GameOverState : IGameState
{
    private GameStateMachine _stateMashine;
    private IGameOverView _gameOverView;

    public GameOverState(GameStateMachine stateMashine, IGameOverView gameOverView)
    {
        _stateMashine = stateMashine;
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