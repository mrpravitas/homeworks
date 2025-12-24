using System;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private float _lifetime;

    public event Action OnHit;

    public void Destroy()
    {
        OnHit.Invoke();
        Destroy(gameObject);
    }

    private void Start()
    {
        Debug.Log("Target created");
        Destroy(gameObject, _lifetime);

        AddRandomBehaviour();
    }

    private void Update()
    {
        Debug.Log("Target still alive");
    }

    private void OnDestroy()
    {
        Debug.Log("Target destroyed");
    }

    private void AddRandomBehaviour()
    {
        int behaviour = UnityEngine.Random.Range(0, 3);

        switch (behaviour)
        {
            case 0:
                gameObject.AddComponent<Scale>();
                break;
            case 1:
                gameObject.AddComponent<Rotate>();
                break;
            case 2:
                gameObject.AddComponent<ColorChange>();
                break;
        }
    }
}
