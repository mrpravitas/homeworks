using System.Collections;
using UnityEngine;

public class NPCWander : MonoBehaviour
{
    [SerializeField] private float _stepDistance; 
    [SerializeField] private float _stepSpeed;

    private Coroutine _wanderCoroutine;

    private void OnEnable()
    {
        _wanderCoroutine = StartCoroutine(Wander());
    }

    private void OnDisable()
    {
        StopCoroutine(_wanderCoroutine);
    }

    private IEnumerator Wander()
    {
        while (true)
        { 
            Vector2 randomDirection = Random.insideUnitCircle; 
            Vector2 targetPosition = transform.position + (Vector3)(randomDirection * _stepDistance);

            while (Vector2.Distance(transform.position, targetPosition) > 0.01f) 
            { 
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, 
                    _stepSpeed * Time.deltaTime); 

                yield return null; 
            } 
            
            yield return new WaitForSeconds(1f); 
        }
    }
}
