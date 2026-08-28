using Mirror;
using System.Collections;
using TMPro;
using UnityEngine;

public class MedKitManager : NetworkBehaviour
{
    [SerializeField] private int _maxMedKits;
    [SerializeField] private Health _health;
    [SerializeField] private int _healAmount;
    [SerializeField] private TextMeshProUGUI _medKitText;
    [SerializeField] private float _healCooldown;
    [SerializeField] private TextMeshProUGUI _noMedkitMessage;
    [SerializeField] private float _messageDuration;

    [SyncVar(hook = nameof(OnMedKitsChanged))]
    private int _medKits;
    private float _lastHealTime;

    private void Awake()
    {
        _lastHealTime = -_healCooldown;
    }

    public void TryPickup(MedKitPickup pickup)
    {
        if (!isOwned)
        {
            return;
        }
        CmdTryPickup(pickup);
    }

    [Command]
    private void CmdTryPickup(MedKitPickup pickup)
    {
        if (!pickup.IsAvailable || _medKits >= _maxMedKits)
        {
            return;
        }

        _medKits = Mathf.Min(_maxMedKits, _medKits + pickup.Amount);
        pickup.SetUnavailable();

        string who = GetComponent<GamePlayer>().Nickname;
        Debug.Log($"{who} picked up medkit");
    }

    private void Update()
    {
        if (!isOwned)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            if (_medKits <= 0)
            {
                StartCoroutine(NoMedkitsCoroutine());
            }
            CmdUseMedKit();
        }
    }

    private IEnumerator NoMedkitsCoroutine()
    {
        _noMedkitMessage.gameObject.SetActive(true);
        yield return new WaitForSeconds(_messageDuration);
        _noMedkitMessage.gameObject.SetActive(false);
    }

    private void OnMedKitsChanged(int oldValue, int newValue)
    {
        _medKitText.text = $"MedKits: {newValue}";
    }

    [Command]
    private void CmdUseMedKit()
    {
        if (_medKits <= 0 || _health.IsFull)
        {
            return;
        }

        if (Time.time - _lastHealTime <= _healCooldown)
        {
            return;
        }

        _medKits--;
        _health.Heal(_healAmount);
        _lastHealTime = Time.time;
    }
}
