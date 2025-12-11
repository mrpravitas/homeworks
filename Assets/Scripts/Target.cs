using System.Collections;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private float _lifetime;

    private void Start()
    {
        Debug.Log("Target created");
        Destroy(gameObject, 3f);
    }

    private void Update()
    {
        Debug.Log("Target still alive");
    }

    private void OnDestroy()
    {
        Debug.Log("Target destroyed");
    }
}
