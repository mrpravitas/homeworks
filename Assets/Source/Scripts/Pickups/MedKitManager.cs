using Mirror;
using TMPro;
using UnityEngine;

public class MedKitManager : NetworkBehaviour
{
    [SerializeField] private int _maxMedKits;
    [SerializeField] private Health _health;
    [SerializeField] private int _healAmount;
    [SerializeField] private TextMeshProUGUI _medKitText;

    [SyncVar(hook = nameof(OnMedKitsChanged))]
    private int _medKits;

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

        _medKits++;
        pickup.SetUnavailable();
    }

    private void Update()
    {
        if (!isOwned)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            CmdUseMedKit();
        }
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

        _medKits--;
        _health.Heal(_healAmount);
    }
}
