using UnityEngine;
using Mirror;
using System.Collections;

public abstract class Pickup : NetworkBehaviour
{
    [SerializeField] private MeshRenderer _renderer;
    [SerializeField] private float _respawnTime;
    [SerializeField] private bool _respawnable;
    [SerializeField] private int _amount;

    [SyncVar(hook = nameof(OnAvailabilityChanged))]
    protected bool _isAvailable = true;

    public bool IsAvailable => _isAvailable;
    public int Amount => _amount;

    private void OnTriggerEnter(Collider other)
    {
        if (!_isAvailable)
        {
            return;
        }

        GamePlayer player = other.GetComponent<GamePlayer>();
        if (player == null)
        {
            return;
        }

        ItemManager manager = GetManager(player);
        if (manager == null)
        {
            return;
        }

        manager.TryPickup(this);
    }

    protected abstract ItemManager GetManager(GamePlayer player);

    [Server]
    public void SetUnavailable()
    {
        _isAvailable = false;

        if (_respawnable)
        {
            StartCoroutine(RespawnCoroutine());
        }
    }

    [Server]
    private IEnumerator RespawnCoroutine()
    {
        yield return new WaitForSeconds(_respawnTime);
        _isAvailable = true;
    }

    private void OnAvailabilityChanged(bool oldValue, bool newValue)
    {
        Color c = _renderer.material.color;

        if (!newValue)
        {
            c.a = 0.3f;
            _renderer.material.color = c;
        }
        else
        {
            c.a = 1f;
            _renderer.material.color = c;
        }
    }
}
