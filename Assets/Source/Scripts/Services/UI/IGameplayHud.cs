using System.Collections.Generic;

public interface IGameplayHud
{
    void ShowModifiers(List<IGameModifier> modifiers);
    void HideModifiers();
}
