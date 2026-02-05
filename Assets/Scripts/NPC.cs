using System.Collections;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [Header("OnWeatherChanged")]
    [SerializeField] private GameObject _umbrella;

    [Header("OnEnemySpotted")]
    [SerializeField] private float _stepAwaySpeed;

    private void OnEnable()
    {
        EventManager.OnGameEvent += HandleEvent;
    }

    private void OnDisable()
    {
        EventManager.OnGameEvent -= HandleEvent;
    }

    private void HandleEvent(GameEvent gameEvent)
    {
        switch (gameEvent.EventType)
        {
            case EventType.WeatherChanged:
                OnWeatherChanged();
                break;
            case EventType.EnemySpotted:
                OnEnemySpotted(gameEvent.Parameter);
                break;
            case EventType.EnemyDefeated:
                OnEnemyDefeated();
                break;
            case EventType.EarthquakeStarted:
                OnEarthquakeStarted(gameEvent.Parameter);
                break;
        }
    }

    private void OnWeatherChanged()
    {
        _umbrella.SetActive(!_umbrella.activeSelf);
    }

    private void OnEnemySpotted(object enemyPosition)
    {
        Vector2 npcPosition = transform.position; 
        Vector2 stepDirection = (npcPosition - (Vector2)enemyPosition).normalized; 

        Vector2 newPosition = npcPosition + stepDirection * 1.5f;

        StartCoroutine(StepAway(newPosition));
    }

    private void OnEnemyDefeated()
    {

    }

    private void OnEarthquakeStarted(object duration)
    {

    }

    private IEnumerator StepAway(Vector2 targetPosition)
    {
        NPCWander wander = GetComponent<NPCWander>();
        if (wander != null)
        {
            wander.enabled = false;
        }

        while (Vector2.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, 
                _stepAwaySpeed * Time.deltaTime);

            yield return null;
        }

        if (wander != null)
        {
            wander.enabled = true;
        }
    }
}
