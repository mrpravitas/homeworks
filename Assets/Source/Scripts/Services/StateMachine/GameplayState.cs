using UnityEngine;

public class GameplayState : IGameState
{
    private GameStateMachine _stateMashine;
    private IGameplayInput _gameplayInput;

    public GameplayState(GameStateMachine stateMashine, IGameplayInput gameplayInput)
    {
        _stateMashine = stateMashine;
        _gameplayInput = gameplayInput;
    }

    public void Enter()
    {
        Time.timeScale = 1f;
        _gameplayInput.SetPauseHandler(OnPause);
    }

    public void Exit()
    {
        _gameplayInput.SetPauseHandler(null);
    }

    private void OnPause()
    {
        _stateMashine.ChangeState<PauseState>();
    }
}
