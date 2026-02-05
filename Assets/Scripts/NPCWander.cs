using System.Collections;
using UnityEngine;

public class NPCWander : MonoBehaviour
{
    [SerializeField] private float _stepDistance; 
    [SerializeField] private float _stepSpeed;

    private void Start() 
    { 
        StartCoroutine(Wander()); 
    }
    private IEnumerator Wander()
    {
        while (true)
        { 
            Vector2 randomDirection = Random.insideUnitCircle; 
            Vector3 targetPos = transform.position + (Vector3)(randomDirection * _stepDistance);

            while (Vector3.Distance(transform.position, targetPos) > 0f) 
            { 
                transform.position = Vector3.MoveTowards(transform.position, targetPos, _stepSpeed * Time.deltaTime); 
                yield return null; 
            } 
            
            yield return new WaitForSeconds(1f); 
        }
    }
}
