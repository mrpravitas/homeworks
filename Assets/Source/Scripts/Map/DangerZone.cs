using System.Collections;
using UnityEngine;

public class DangerZone : MonoBehaviour
{
    [SerializeField] private int _damagePerTick;
    [SerializeField] private float _timePerTick;

    private Coroutine _damageCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        
        if (player != null )
        {
            _damageCoroutine = StartCoroutine(DamageCoroutine(player));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();

        if (player != null && _damageCoroutine != null)
        {
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
