using Mirror;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Health : NetworkBehaviour
{
    [SerializeField] private int _maxHealth;

    [Header("UI")]
    [SerializeField] private TextMeshPro _healthLabel;
    [SerializeField] private TextMeshProUGUI _healthBar;
    [Header("Respawn")]
    [SerializeField] private float _respawnDelay;
    [SerializeField] private PlayerController _controller;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private MeshRenderer _meshRenderer;

    [SyncVar(hook = nameof(OnHpChanged))]
    private int _currentHealth;
    private bool _isDead;

    public bool IsFull => _currentHealth >= _maxHealth;

    public static event Action<uint, uint> OnPlayerDied;

    public override void OnStartServer()
    {
        _currentHealth = _maxHealth;
        _isDead = false;
    }

    [Server]
    public void TakeDamage(int damageAmount, uint attackerNetId = 0)
    {
        if (damageAmount <= 0 || _isDead)
        {
            return;
        }

        _currentHealth -= damageAmount;

        if (_currentHealth <= 0)
        {
            _isDead = true;
            OnPlayerDied?.Invoke(netId, attackerNetId);
            RpcRespawn();
        }
    }

    [Server]
    public void Heal(int amount)
    {
        if (amount <= 0)
        {
            return;
        }

        _currentHealth += amount;

        if (_currentHealth >  _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
    }

    [ClientRpc]
    private void RpcRespawn()
    {
        StartCoroutine(RespawnCoroutine());
    }

    private IEnumerator RespawnCoroutine()
    {
        if (isOwned)
        {
            _controller.enabled = false;
            _weapon.enabled = false;
        }

        _characterController.enabled = false;
        _meshRenderer.enabled = false;

        yield return new WaitForSeconds(_respawnDelay);

        Transform start = NetworkManager.singleton.GetStartPosition();
        transform.position = start.position;

        if (isOwned)
        {
            _controller.enabled = true;
            _weapon.enabled = true;
        }

        _characterController.enabled = true;
        _meshRenderer.enabled = true;

        CmdResetHealth();
    }

    [Command]
    private void CmdResetHealth()
    {
        _currentHealth = _maxHealth;
        _isDead = false;
    }

    private void OnHpChanged(int oldHp, int newHp)
    {
        if (newHp <= 0)
        {
            _healthLabel.text = "Respawning...";
            _healthBar.text = "Respawning...";
            return;
        }

        string text = $"{newHp}/{_maxHealth}";

        _healthLabel.text = text;
        _healthBar.text = $"HP: {text}";
    }
}
