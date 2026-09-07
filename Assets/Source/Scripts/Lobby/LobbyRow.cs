using TMPro;
using UnityEngine;

public class LobbyRow : MonoBehaviour
{
    [SerializeField] private TMP_Text _nickname;

    public void Set(PlayerInfo info)
    {
        _nickname.text = info.IsReady ? $"{info.Nickname} [Ready]" : info.Nickname;
        _nickname.color = info.Color;
    }
}