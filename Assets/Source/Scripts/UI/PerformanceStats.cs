using TMPro;
using Unity.Profiling;
using UnityEngine;

public class PerformanceStats : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private float _updateInterval;

    private ProfilerRecorder _gcAllocRecorder;
    private long _allocated;
    private float _elapsed;
    private int _frames;
    private float _totalTime;
    private int _totalFrames;

    private void OnEnable()
    {
        _gcAllocRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "GC Allocated In Frame");
        SpawnModeSwitcher.ModeChanged += OnModeChanged;
    }

    private void OnDisable()
    {
        _gcAllocRecorder.Dispose();
        SpawnModeSwitcher.ModeChanged -= OnModeChanged;
    }

    private void OnModeChanged(SpawnMode mode)
    {
        _allocated = 0;
        _frames = 0;
        _elapsed = 0f;
        _totalFrames = 0;
        _totalTime = 0f;
    }

    private void Update()
    {
        _frames++;
        _totalFrames++;
        _elapsed += Time.unscaledDeltaTime;
        _totalTime += Time.unscaledDeltaTime;
        _allocated += _gcAllocRecorder.LastValue;

        if (_elapsed < _updateInterval)
        {
            return;
        }

        float allocatedKb = _allocated / 1024f;
        float fps = _frames / _elapsed;
        float averageFps = _totalFrames / _totalTime;

        _text.text = $"FPS: {fps:0}\nAVG FPS: {averageFps:0}\nGC Alloc: {allocatedKb:0.0} KB / {_updateInterval}s";

        _allocated = 0;
        _frames = 0;
        _elapsed = 0f;
    }
}
