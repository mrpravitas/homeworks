using UnityEngine;
using Mirror;
using System.Collections;

public class MedKitPickup : NetworkBehaviour
{
    [SerializeField] private MeshRenderer _renderer;
    [SerializeField] private float _respawnTime;
    [SerializeField] private bool _respawnable;
    [SerializeField] private int _amount;

    [SyncVar(hook = nameof(OnAvailabilityChanged))]
    private bool _isAvailable = true;

    public bool IsAvailable => _isAvailable;
    public int Amount => _amount;

    private void OnTriggerEnter(Collider other)
    {
        if (!_isAvailable) return;

        GamePlayer player = other.GetComponent<GamePlayer>();
        if (player == null) return;

        MedKitManager medKitManager = player.GetComponent<MedKitManager>();
        if (medKitManager == null) return;

        medKitManager.TryPickup(this);  
    }

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
