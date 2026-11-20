using System;

public readonly struct GameStartedSignal
{
}

public readonly struct InputSwitchedSignal
{
    public readonly string InputName;

    public InputSwitchedSignal(string inputName)
    {
        InputName = inputName;
    }
}

public readonly struct PlayerDamagedSignal
{
    public readonly int Amount;
    public readonly string Reason;

    public PlayerDamagedSignal(int amount, string reason)
    {
        Amount = amount;
        Reason = reason;
    }
}

public readonly struct HealthPickupCollectedSignal
{
    public readonly int Amount;

    public HealthPickupCollectedSignal(int amount)
    {
        Amount = amount;
    }
}

public readonly struct ZoneEventSignal
{
    public readonly string Message;

    public ZoneEventSignal(string message)
    {
        Message = message;
    }
}

public readonly struct ChangeStateSignal
{
    public readonly Type StateType;

    public ChangeStateSignal(Type stateType)
    {
        StateType = stateType;
    }
}
