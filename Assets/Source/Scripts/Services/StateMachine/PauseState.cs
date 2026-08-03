using UnityEngine;

public class PauseState : IGameState
{
    private GameStateMachine _stateMashine;

    public PauseState(GameStateMachine stateMashine)
    {
        _stateMashine = stateMashine;
    }

    public void Enter()
    {
        Debug.Log("PauseState: Enter");
    }

    public void Exit()
    {
        Debug.Log("PauseState: Exit");
    }
}