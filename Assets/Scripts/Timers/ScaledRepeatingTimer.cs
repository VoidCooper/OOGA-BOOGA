using UnityEngine;

internal class ScaledRepeatingTimer : RepeatingTimer
{
    private void Update()
    {
        if (!IsRunning) return;

        _timer += Time.deltaTime;
        if (_timer >= Duration)
        {
            _timer -= Duration;
            TimesCompleted++;
            CompletedTimer(false);
        }
    }
}
