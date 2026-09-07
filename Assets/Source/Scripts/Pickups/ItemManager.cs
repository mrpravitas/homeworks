using Mirror;
using System.Collections;
using TMPro;
using UnityEngine;

public abstract class ItemManager : NetworkBehaviour
{
    [SerializeField] private int _maxAmount;
    [SerializeField] private TextMeshProUGUI _amountText;
    [SerializeField] private float _cooldown;
    [SerializeField] private TextMeshProUGUI _noItemMessage;
    [SerializeField] private float _messageDuration;
    [SerializeField] private KeyCode _useKey;
    [SerializeField] private string _itemLabel;
    [SerializeField] private string _pickupName;

    [SyncVar(hook = nameof(OnAmountChanged))]
    protected int _amount;
    private float _lastUseTime;

    private void Awake()
    {
        _lastUseTime = -_cooldown;
    }

    public void TryPickup(Pickup pickup)
    {
        if (!isOwned)
        {
            return;
        }
        CmdTryPickup(pickup);
    }

    [Command]
    private void CmdTryPickup(Pickup pickup)
    {
        if (!pickup.IsAvailable || _amount >= _maxAmount)
        {
            return;
        }

        _amount = Mathf.Min(_maxAmount, _amount + pickup.Amount);
        pickup.SetUnavailable();

        string who = GetComponent<GamePlayer>().Nickname;
        Debug.Log($"{who} picked up {_pickupName}");
    }

    private void Update()
    {
        if (!isOwned)
        {
            return;
        }

        if (Input.GetKeyDown(_useKey))
        {
            if (_amount <= 0)
            {
                StartCoroutine(ShowNoItemMessage());
            }
            CmdUse();
        }
    }

    [Command]
    private void CmdUse()
    {
        if (_amount <= 0)
        {
            return;
        }

        if (!CanUse())
        {
            return;
        }

        if (Time.time - _lastUseTime <= _cooldown)
        {
            return;
        }

        _amount--;
        _lastUseTime = Time.time;
        Use();
    }

    protected virtual bool CanUse()
    {
        return true;
    }

    protected abstract void Use();

    private IEnumerator ShowNoItemMessage()
    {
        _noItemMessage.gameObject.SetActive(true);
        yield return new WaitForSeconds(_messageDuration);
        _noItemMessage.gameObject.SetActive(false);
    }

    private void OnAmountChanged(int oldValue, int newValue)
    {
        _amountText.text = $"{_itemLabel}: {newValue}";
    }
}