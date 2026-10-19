using System.Collections.Generic;
using UnityEngine;

public class TickSystem : ITickable
{
    private readonly List<ITickable> _tickables = new();
    private readonly List<int> _multipliers = new();
    private readonly List<int> _frames = new();
    private readonly float _nearBoundary;
    private readonly float _farBoundary;

    public TickSystem(float nearBoundary, float farBoundary)
    {
        _nearBoundary = nearBoundary;
        _farBoundary = farBoundary;
    }

    public void Register(ITickable tickable)
    {
        _tickables.Add(tickable);
        _multipliers.Add(1);
        _frames.Add(0);
    }

    public void Unregister(ITickable tickable)
    {
        int index = _tickables.IndexOf(tickable);

        if (index >= 0)
        {
            _tickables.RemoveAt(index);
            _multipliers.RemoveAt(index);
            _frames.RemoveAt(index);
        }
    }

    public void Tick(float deltaTime)
    {
        for (int i = _tickables.Count - 1; i >= 0; i--)
        {
            ITickable tickable = _tickables[i];

            if (tickable == null)
            {
                _tickables.RemoveAt(i);
                _multipliers.RemoveAt(i);
                _frames.RemoveAt(i);
                continue;
            }

            _frames[i]++;

            if (_frames[i] < _multipliers[i])
            {
                continue;
            }

            int multiplier = _multipliers[i];
            _frames[i] = 0;
            _multipliers[i] = GetMultiplier(tickable);
            tickable.Tick(deltaTime * multiplier);
        }
    }

    private int GetMultiplier(ITickable tickable)
    {
        if (tickable is not Component component)
        {
            return 1;
        }

        float z = component.transform.position.z;

        if (z > _farBoundary)
        {
            return 3;
        }

        if (z > _nearBoundary)
        {
            return 2;
        }

        return 1;
    }
}
