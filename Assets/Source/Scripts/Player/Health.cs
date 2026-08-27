using UnityEngine;
using Mirror;
using TMPro;
using System.Collections;

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
            RpcRespawn();
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

        _currentHealth = _maxHealth;
    }

    private void OnHpChanged(int oldHp, int newHp)
    {
        string text = $"{newHp}/{_maxHealth}";

        _healthLabel.text = text;
        _healthBar.text = $"HP: {text}";
    }
}
