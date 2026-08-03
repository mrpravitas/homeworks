using System;

public interface IMainMenuView
{
    void SetStartHandler(Action handler);
    void SetExitHandler(Action handler);
}