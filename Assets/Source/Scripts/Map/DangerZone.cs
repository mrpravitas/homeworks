using System;
using System.Collections;
using UnityEngine;

public class DangerZone : MonoBehaviour, IEntityWithConfig
{
    public static event Action<string> OnEvent;

    private int _damagePerTick; 
    private float _timePerTick;

    private Coroutine _damageCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        
        if (player != null )
        {
            OnEvent?.Invoke("You enter danger zone");
            _damageCoroutine = StartCoroutine(DamageCoroutine(player));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();

        if (player != null && _damageCoroutine != null)
        {
            OnEvent?.Invoke("You exit danger zone");

            StopCoroutine(_damageCoroutine);
            _damageCoroutine = null;
        }
    }

    private IEnumerator DamageCoroutine(PlayerController player)
    {
        while (true)
        {
            yield return new WaitForSeconds(_timePerTick);
            player.TakeDamage(_damagePerTick);
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
