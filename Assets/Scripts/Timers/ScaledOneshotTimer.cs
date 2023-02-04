using UnityEngine;

public class ScaledOneshotTimer : BaseTimer
{
    private void Update()
    {
        if (!IsRunning) return;

        _timer += Time.deltaTime;
        if (_timer >= Duration)
        {
            _timer = Duration;
            IsRunning = false;
            CompletedTimer(true);
        }
    }
}
