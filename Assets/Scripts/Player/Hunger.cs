using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunger : MonoBehaviour
{
    public float MaxHunger = 100;
    public float CurrentHunger;
    public float StarveSpeed = 1f;
    public float StarveWhileHealing = 0.5f;
    public float HealWithHunger = 10f;
    public float DamageWithHungerSpeed = 1.5f;

    public event System.Action TookFood;
    public event System.Action<bool> CanHealWithFood;
    private bool drainPaused = false;
    private bool _canHealWithFood = true;
    private ScaledRepeatingTimer _timer;

    Health health;

    public float GetNormalizedHunger()
    {
        return Mathf.Round(CurrentHunger);
    }

    private void Awake()
    {
        CurrentHunger = MaxHunger;
        health = GetComponent<Health>();
        _timer = gameObject.AddComponent<ScaledRepeatingTimer>();
        _timer.OnTimerCompleted += HealWithFood;
    }

    private void Start()
    {
        GameManager.Instance.OnGamePaused += OnGamePaused;
        GameManager.Instance.OnGameUnPaused += OnGameUnPaused;
        _timer.StartTimer(2f);
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnGamePaused -= OnGamePaused;
        GameManager.Instance.OnGameUnPaused -= OnGameUnPaused;
    }

    private void OnGameUnPaused()
    {
        drainPaused = false;
    }

    private void OnGamePaused()
    {
        drainPaused = true;
    }

    public void Update()
    {
        if (GameManager.Instance.GamePaused)
            return;
        if (drainPaused)
            return;

        float healAmount = 0;
        CurrentHunger -= StarveSpeed * Time.deltaTime;
        CurrentHunger = Mathf.Clamp(CurrentHunger, 0, MaxHunger);

        if (CurrentHunger == 0)
        {
            healAmount += DamageWithHungerSpeed * Time.deltaTime;
        }

        if (CurrentHunger > (MaxHunger / 2))
        {
            _canHealWithFood = true;
            CanHealWithFood?.Invoke(true);
        }
        else
        {
            _canHealWithFood = false;
            CanHealWithFood?.Invoke(false);
        }


        health.DealDamageWithoutScratch(healAmount);
    }

    public void HealWithFood()
    {
        if (_canHealWithFood && health.CurrentHealth < health.MaxHealth)
        {
            float healAmount = HealWithHunger;
            if ((health.CurrentHealth + healAmount) > health.MaxHealth)
                healAmount = health.MaxHealth - health.CurrentHealth;


            CurrentHunger -= HealWithHunger;
            health.DealDamage(-healAmount);
        }
    }

    public void EatFood(float amount)
    {
        CurrentHunger += amount;
        TookFood?.Invoke();
    }
}
