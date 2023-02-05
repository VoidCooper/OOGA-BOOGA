using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance
    {
        get
        {
            if (m_instance == null)
            {
                InstantiateSelf();
            }
            return m_instance;
        }
    }
    private static GameManager m_instance;
    private float _gameSpeed;
    private float _unpausedSpeed;
    private float _gameLength = 180;
    private ScaledOneshotTimer _gameTimer;
    private bool _gamePaused = false;

    public float gameLength { get { return _gameLength; } }
    public float GameSpeed { get { return _gameSpeed; } }
    public bool GamePaused { get { return _gamePaused; } }

    public float RemainingTime { get { return _gameTimer.TimeLeft; } }
    
    // Events
    public event System.Action OnGameUnPaused;
    public event System.Action OnGamePaused;
    public event System.Action<float> OnGameSpeedChanged;
    public event System.Action OnEndIsNigh;

    private void Awake()
    {
        _gameTimer = gameObject.AddComponent<ScaledOneshotTimer>();
        _gameTimer.OnTimerCompleted += BeginTheEndofTime;
        _gameTimer.StartTimer(_gameLength);
    }

    private void OnDestroy()
    {
        _gameTimer.OnTimerCompleted -= BeginTheEndofTime;
    }

    public void BeginTheEndofTime()
    {
        Debug.Log("THE END IS NIGH!");
        OnEndIsNigh?.Invoke();
    }

    public void PauseGame()
    {
        _unpausedSpeed = _gameSpeed;
        _gameSpeed = 0f;
        _gamePaused = true;
        _gameTimer.StopTimer();
        OnGamePaused?.Invoke();
    }

    public void UnPauseGame()
    {
        _gameSpeed = _unpausedSpeed;
        OnGameUnPaused?.Invoke();
        _gamePaused = false;
        _gameTimer.ResumeTimer();
    }

    public void ChangeGameSpeed(float speed)
    {
        _gameSpeed = speed;
        OnGameSpeedChanged?.Invoke(speed);
    }

    private static void InstantiateSelf()
    {
        if (m_instance != null)
            return;

        GameObject go = new GameObject("GameManager");
        GameManager grm = go.AddComponent<GameManager>();
        m_instance = grm;

        grm._gameSpeed = 1f;
    }

}
