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
