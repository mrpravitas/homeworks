using System;

public interface IPauseView
{
    void SetResumeHandler(Action handler);
    void SetExitToMenuHandler(Action handler);
}
