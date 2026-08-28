using Mirror;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class MatchTimer : NetworkBehaviour
{
    [SerializeField] private int _matchDuration;
    [SerializeField] private TextMeshProUGUI _timerText;

    [SyncVar(hook = nameof(OnTimeChanged))]
    private int _remainingSeconds;

    public static event Action OnGameFinished;

    public override void OnStartServer()
    {
        _remainingSeconds = _matchDuration;
        StartCoroutine(Tick());
    }

    [Server]
    private IEnumerator Tick() 
    {
        while (_remainingSeconds > 0)
        {
            yield return new WaitForSeconds(1f);
            _remainingSeconds--;
        }
        OnGameFinished?.Invoke();
    }

    private void OnTimeChanged(int old, int newSeconds)
    {
        if (newSeconds <= 0)
        {
            _timerText.gameObject.SetActive(false);
            return;
        }

        _timerText.gameObject.SetActive(true);
        _timerText.text = string.Format("{0:00}:{1:00}", newSeconds / 60, newSeconds % 60);
    }
}
