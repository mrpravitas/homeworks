using UnityEngine;
using Mirror;
using TMPro;

public class Health : NetworkBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private TextMeshPro _healthLabel;
    [SerializeField] private TextMeshProUGUI _healthBar;

    [SyncVar(hook = nameof(OnHpChanged))]
    private int _currentHealth;

    public override void OnStartServer()
    {
        _currentHealth = _maxHealth;
    }

    [Server]
    public void TakeDamage(int damageAmount)
    {
        if (damageAmount <= 0)
        {
            return;
        }

        _currentHealth -= damageAmount;

        if (_currentHealth <= 0)
        {
            _currentHealth = _maxHealth;
            RpcRespawn();
        }
    }

    [ClientRpc]
    private void RpcRespawn()
    {
        Transform start = NetworkManager.singleton.GetStartPosition();

        CharacterController cc = GetComponent<CharacterController>();
        cc.enabled = false;
        transform.position = start.position;
        cc.enabled = true;
    }

    private void OnHpChanged(int oldHp, int newHp)
    {
        string text = $"{newHp}/{_maxHealth}";

        _healthLabel.text = text;
        _healthBar.text = $"HP: {text}";
    }
}
