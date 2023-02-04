using UnityEngine;

internal class UnScaledRepeatingTimer : RepeatingTimer
{
    private void Update()
    {
        if (!IsRunning) return;

        _timer += Time.unscaledDeltaTime;
        if (_timer >= Duration)
        {
            _timer -= Duration;
            TimesCompleted++;
            CompletedTimer(false);
        }
    }
}
