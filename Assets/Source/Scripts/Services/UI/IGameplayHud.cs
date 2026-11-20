using System.Collections.Generic;

public interface IGameplayHud
{
    void ShowModifiers(IReadOnlyList<IGameModifier> modifiers);
    void HideModifiers();
}
