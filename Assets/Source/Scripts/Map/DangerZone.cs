using System.Collections;
using UnityEngine;
using VContainer;

public class DangerZone : MonoBehaviour, IEntityWithConfig
{
    private IMessageBus _bus;
    private int _damagePerTick;
    private float _timePerTick;

    private Coroutine _damageCoroutine;

    [Inject]
    public void Construct(IMessageBus bus)
    {
        _bus = bus;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() == null)
        {
            return;
        }

        _bus.Publish(new ZoneEventSignal("You enter danger zone"));
        _damageCoroutine = StartCoroutine(DamageCoroutine());
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>() == null || _damageCoroutine == null)
        {
            return;
        }

        _bus.Publish(new ZoneEventSignal("You exit danger zone"));

        StopCoroutine(_damageCoroutine);
        _damageCoroutine = null;
    }

    private IEnumerator DamageCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_timePerTick);
            _bus.Publish(new PlayerDamagedSignal(_damagePerTick, "Danger zone"));
        }
    }

    public void Init(ScriptableObject config)
    {
        DangerZoneConfig dangerZoneConfig = config as DangerZoneConfig;

        _damagePerTick = dangerZoneConfig.DamagePerTick;
        _timePerTick = dangerZoneConfig.TimePerTick;

        transform.localScale *= dangerZoneConfig.ScaleMultiplier;
    }
}
