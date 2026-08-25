using Mirror;
using TMPro;
using UnityEngine;

public class GamePlayer : NetworkBehaviour
{
    [SerializeField] private TextMeshPro _nicknameLabel;
    [SerializeField] private MeshRenderer _playerRenderer;

    [SyncVar]
    private string _nickname;
    [SyncVar]
    private Color _color;

    public void SetData(string nickname, Color color)
    {
        _nickname = nickname;
        _color = color;
    }

    public override void OnStartClient()
    {
        ApplyData();
    }

    private void ApplyData()
    {
        _nicknameLabel.text = _nickname;
        _nicknameLabel.color = _color;

        _playerRenderer.material.color = _color;
    }
}
