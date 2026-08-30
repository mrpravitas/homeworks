using Mirror;
using System.Collections;
using TMPro;
using UnityEngine;

public class GrenadeManager : NetworkBehaviour
{
    [SerializeField] private int _maxGrenades;
    [SerializeField] private TextMeshProUGUI _grenadeText;
    [SerializeField] private float _grenadeCooldown;
    [SerializeField] private Grenade _grenadePrefab;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _throwSpeed;
    [SerializeField] private TextMeshProUGUI _noGrenadeMessage;
    [SerializeField] private float _messageDuration;

    [SyncVar(hook = nameof(OnGrenadesChanged))]
    private int _grenades;
    private float _lastGrenadeTime;

    private void Awake()
    {
        _lastGrenadeTime = -_grenadeCooldown;
    }

    public void TryPickup(GrenadePickup pickup)
    {
        if (!isOwned)
        {
            return;
        }
        CmdTryPickup(pickup);
    }

    [Command]
    private void CmdTryPickup(GrenadePickup pickup)
    {
        if (!pickup.IsAvailable || _grenades >=  _maxGrenades)
        {
            return;
        }

        _grenades = Mathf.Min(_maxGrenades, _grenades + pickup.Amount);
        pickup.SetUnavailable();

        string who = GetComponent<GamePlayer>().Nickname;
        Debug.Log($"{who} picked up grenade");
    }

    private void Update()
    {
        if (!isOwned)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            if (_grenades <= 0)
            {
                StartCoroutine(NoGrenadesCoroutine());
            }
            CmdThrowGrenade();
        }
    }

    private IEnumerator NoGrenadesCoroutine()
    {
        _noGrenadeMessage.gameObject.SetActive(true);
        yield return new WaitForSeconds(_messageDuration);
        _noGrenadeMessage.gameObject.SetActive(false);
    }

    private void OnGrenadesChanged(int oldValue, int newValue)
    {
        _grenadeText.text = $"Grenades: {newValue}";
    }

    [Command]
    private void CmdThrowGrenade()
    {
        if (_grenades <= 0)
        {
            return;
        }

        if (Time.time - _lastGrenadeTime <= _grenadeCooldown)
        {
            return;
        }

        _grenades--;
        _lastGrenadeTime = Time.time;

        Vector3 origin = _camera.transform.position;
        Vector3 direction = _camera.transform.forward;

        Grenade grenade = Instantiate(_grenadePrefab, origin, Quaternion.identity);
        NetworkServer.Spawn(grenade.gameObject);
        grenade.Launch(direction * _throwSpeed, netId);

        string who = GetComponent<GamePlayer>().Nickname;
        Debug.Log($"{who} threw a grenade");
    }
}
