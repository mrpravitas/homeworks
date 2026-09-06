using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : NetworkBehaviour
{
    [SerializeField] private float _fuseTime;
    [SerializeField] private float _blastRadius;
    [SerializeField] private int _maxDamage;
    [SerializeField] private int _minDamage;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private LayerMask _targetMask;
    [SerializeField] private GameObject _explosionSphere;
    [SerializeField] private float _explosionVisualDuration;
    [SerializeField] private AudioClip _explosionSound;

    private uint _ownerNetId;

    [Server]
    public void Launch(Vector3 velocity, uint ownerNetId)
    {
        _ownerNetId = ownerNetId;
        _rigidbody.velocity = velocity;
        StartCoroutine(LaunchCoroutine());
    }

    [Server]
    private IEnumerator LaunchCoroutine()
    {
        yield return new WaitForSeconds(_fuseTime);
        StartCoroutine(ExplodeCoroutine());
    }

    [Server]
    private IEnumerator ExplodeCoroutine()
    {
        HashSet<Health> affected = new();

        Collider[] hits = Physics.OverlapSphere(transform.position, _blastRadius, _targetMask);
        foreach (Collider hit in hits)
        {
            Health health = hit.GetComponentInParent<Health>();
            if (health == null || !affected.Add(health))
            {
                continue;
            }

            float distance = Vector3.Distance(transform.position, hit.transform.position);
            float normalizedDistance = Mathf.Clamp01(distance / _blastRadius);
            float damageFalloff = 1f - normalizedDistance;
            int damage = (int)Mathf.Lerp(_minDamage, _maxDamage, damageFalloff);
            health.TakeDamage(damage, _ownerNetId);

            GamePlayer player = health.GetComponent<GamePlayer>();
            string who = player.Nickname;
            Debug.Log($"{who} took {damage} damage from grenade explosion");
        }

        RpcShowExplosion();
        RpcPlayExplosionSound();

        yield return new WaitForSeconds(_explosionVisualDuration);

        NetworkServer.Destroy(gameObject);
    }

    [ClientRpc]
    private void RpcShowExplosion()
    {
        Vector3 targetWorldScale = Vector3.one * (_blastRadius * 2f);
        Transform parent = _explosionSphere.transform.parent;
        Vector3 parentScale = parent ? parent.lossyScale : Vector3.one;

        _explosionSphere.transform.localScale = new Vector3(
            targetWorldScale.x / parentScale.x,
            targetWorldScale.y / parentScale.y,
            targetWorldScale.z / parentScale.z
        );

        _explosionSphere.SetActive(true);
    }

    [ClientRpc]
    private void RpcPlayExplosionSound()
    {
        AudioSource.PlayClipAtPoint(_explosionSound, transform.position);
    }
}
