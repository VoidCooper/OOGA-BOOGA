using UnityEngine;

public class UnScaledOneshotTimer : BaseTimer
{
    private void Update()
    {
        if (!IsRunning) return;

        _timer += Time.unscaledDeltaTime;
        if (_timer >= Duration)
        {
            _timer = Duration;
            IsRunning = false;
            CompletedTimer(true);
        }
    }
}
