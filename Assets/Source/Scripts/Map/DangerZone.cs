using System;
using System.Collections;
using UnityEngine;

public class DangerZone : MonoBehaviour
{
    public static event Action<string> OnEvent;

    [SerializeField] private int _damagePerTick;
    [SerializeField] private float _timePerTick;

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
}
