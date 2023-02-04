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

    public float GameSpeed { get { return _gameSpeed; } }

    // Events
    public event System.Action OnGameUnPaused;
    public event System.Action OnGamePaused;
    public event System.Action<float> OnGameSpeedChanged;


    public void PauseGame()
    {
        _unpausedSpeed = _gameSpeed;
        _gameSpeed = 0f;
        OnGamePaused?.Invoke();
    }

    public void UnPauseGame()
    {
        _gameSpeed = _unpausedSpeed;
        OnGameUnPaused?.Invoke();
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

        GameObject go = new GameObject("GlobalReferenceManager");
        GameManager grm = go.AddComponent<GameManager>();
        m_instance = grm;

        grm._gameSpeed = 1f;
    }

}
