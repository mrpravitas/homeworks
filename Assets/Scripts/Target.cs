using System.Collections;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private float _lifetime;

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
        int behaviour = Random.Range(0, 3);

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
