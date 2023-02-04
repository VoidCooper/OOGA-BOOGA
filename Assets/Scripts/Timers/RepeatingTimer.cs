internal abstract class RepeatingTimer : BaseTimer
{
    /// <summary>
    /// How many times the timer has completed.
    /// </summary>
    public int TimesCompleted { get; protected set; } = 0;

    /// <summary>
    /// How many seconds total the timer has run.
    /// </summary>
    public float TotalTimeElapsed { get { return TimesCompleted * Duration + TimeElapsed; } }

    public override void StartTimer(float duration)
    {
        TimesCompleted = 0;
        base.StartTimer(duration);
    }
}
