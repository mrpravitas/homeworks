using UnityEngine;

public class GameOverState : IGameState
{
    private GameStateMachine _stateMashine;

    public GameOverState(GameStateMachine stateMashine)
    {
        _stateMashine = stateMashine;
    }

    public void Enter()
    {
        Debug.Log("GameOverState: Enter");
    }

    public void Exit()
    {
        Debug.Log("GameOverState: Exit");
    }
}