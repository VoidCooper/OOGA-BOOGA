using System;
using UnityEngine;

public class BaseTimer : MonoBehaviour
{
    public event Action OnTimerCompleted;
    public float TimeElapsed { get { return _timer; } }
    public float TimeLeft { get { return Duration - _timer; } }
    public float NormalizedTimeElapsed { get { return TimeElapsed / Duration; } }
    public float NormalizedTimeLeft { get { return TimeLeft / Duration; } }
    public bool IsRunning { get; protected set; } = false;
    public float Duration
    {
        get
        {
            return _duration;
        }
        protected set
        {
            if (value < 0.0f)
            {
                Debug.LogError("Invalid duration!!!");
            }
            else
            {
                _duration = value;
            }
        }
    }

    protected float _timer = 0.0f;
    private float _duration = 0.0f;

    protected void CompletedTimer(bool stopRunning)
    {
        IsRunning = !stopRunning;
        enabled = !stopRunning;
        OnTimerCompleted?.Invoke();
    }

    public virtual void StartTimer(float duration)
    {
        IsRunning = true;
        enabled = true;
        Duration = duration;
        if (Duration <= 0.0f)
        {
            Debug.LogWarning($"Timer duration is negative or 0. Duration:{Duration}");
        }
        _timer = 0.0f;
    }
    public void ResumeTimer()
    {
        if (_timer < Duration)
        {
            IsRunning = true;
            enabled = true;
        }
        else
        {
            Debug.LogWarning("Timer has already completed. Start timer again instead.");
        }
    }
    public void StopTimer()
    {
        IsRunning = false;
        enabled = false;
    }

    protected virtual void Start()
    {
        enabled = IsRunning;
    }

    protected virtual void OnDestroy()
    {
        OnTimerCompleted = null;
    }
}
