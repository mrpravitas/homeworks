using UnityEngine;
using UnityEngine.Events;

public class StatsLoader : MonoBehaviour
{
    private int _record;
    private int _totalTarget;
    private int _totalShots;

    public UnityEvent<int> OnRecordUpdated;
    public UnityEvent<int> OnTotalTargetUpdated;
    public UnityEvent<int> OnTotalShotsUpdated;

    private void Start()
    {
        LoadStats();
        UpdateStats();
    }

    private void LoadStats()
    {
        _record = PlayerPrefs.GetInt("best hit count", 0);
        _totalTarget = PlayerPrefs.GetInt("destroyed targets", 0);
        _totalShots = PlayerPrefs.GetInt("shot count", 0);
    }

    private void UpdateStats()
    {
        OnRecordUpdated.Invoke(_record);
        OnTotalTargetUpdated.Invoke(_totalTarget);
        OnTotalShotsUpdated.Invoke(_totalShots);
    }
}
