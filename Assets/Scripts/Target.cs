using System.Collections;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private float _lifetime;

    private void Start()
    {
        Debug.Log("Start() was called");
        Destroy(gameObject, 3f);
    }

    private void Update()
    {
        Debug.Log("Update() was called");
    }

    private void OnDestroy()
    {
        Debug.Log("OnDestroy() was called");
    }
}
