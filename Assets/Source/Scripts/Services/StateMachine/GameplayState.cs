using UnityEngine;

public class GameplayState : IGameState
{
    private GameStateMachine _stateMashine;

    public GameplayState(GameStateMachine stateMashine)
    {
        _stateMashine = stateMashine;
    }

    public void Enter()
    {
        Debug.Log("GameplayState: Enter");
    }

    public void Exit()
    {
        Debug.Log("GameplayState: Exit");
    }
}
