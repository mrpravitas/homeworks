using System.Collections;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [Header("OnWeatherChanged")]
    [SerializeField] private GameObject _umbrella;

    [Header("OnEnemySpotted")]
    [SerializeField] private float _stepSpeed;

    private float _stepAwayLength = 1.5f;

    [Header("OnEnemyDefeated")]
    [SerializeField] private GameObject _joyParticles;

    private float _particleRiseSpeed = 0.5f;
    private float _particleRiseHeight = 0.5f;
    private float _stepToEnemyLength = 0.75f;

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
                OnEnemyDefeated(gameEvent.Parameter);
                break;
            case EventType.EarthquakeStarted:
                OnEarthquakeStarted(gameEvent.Parameter);
                break;
        }
    }

    private void OnWeatherChanged()
    {
        _umbrella?.SetActive(!_umbrella.activeSelf);
    }

    private void OnEnemySpotted(object enemyPosition)
    {
        Vector2 npcPosition = transform.position; 
        Vector2 stepDirection = (npcPosition - (Vector2)enemyPosition).normalized; 

        Vector2 newPosition = npcPosition + stepDirection * _stepAwayLength;

        StartCoroutine(StepTo(newPosition));
    }

    private void OnEnemyDefeated(object enemyPosition)
    {
        Vector2 npcPosition = transform.position;
        Vector2 stepDirection = ((Vector2)enemyPosition - npcPosition).normalized;

        Vector2 newPosition = npcPosition + stepDirection * _stepToEnemyLength;

        StartCoroutine(StepTo(newPosition));
        StartCoroutine(Joy());
    }

    private void OnEarthquakeStarted(object duration)
    {

    }

    private IEnumerator StepTo(Vector2 targetPosition)
    {
        NPCWander wander = GetComponent<NPCWander>();
        if (wander != null)
        {
            wander.enabled = false;
        }

        while (Vector2.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, 
                _stepSpeed * Time.deltaTime);

            yield return null;
        }

        if (wander != null)
        {
            wander.enabled = true;
        }
    }

    private IEnumerator Joy()
    {
        if (_joyParticles == null)
        {
            yield break;
        }

        Transform particles = _joyParticles.transform;

        Vector2 startParticlesPosition = particles.localPosition;
        Vector2 endParticlesPosition = startParticlesPosition + Vector2.up * _particleRiseHeight;

        _joyParticles.SetActive(true);

        while (Vector2.Distance(particles.localPosition, endParticlesPosition) > 0.01f)
        {
            particles.localPosition = Vector2.MoveTowards(particles.localPosition, endParticlesPosition,
                _particleRiseSpeed * Time.deltaTime);

            yield return null;
        }

        _joyParticles.SetActive(false);
        particles.localPosition = startParticlesPosition;
    }
}
