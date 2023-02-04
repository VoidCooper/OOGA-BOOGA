using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunger : MonoBehaviour
{
    public float MaxHunger = 100;
    public float CurrentHunger;
    public float StarveSpeed = 1f;
    public float StarveWhileHealing = 0.5f;
    public float HealWithHungerSpeed = 0.25f;
    public float DamageWithHungerSpeed = 1.5f;

    public event System.Action TookFood;
    private bool drainPaused = false;

    Health health;

    public float GetNormalizedHunger()
    {
        return Mathf.Round(CurrentHunger);
    }

    private void Awake()
    {
        CurrentHunger = MaxHunger;
        health = GetComponent<Health>();
    }

    private void Start()
    {
        GameManager.Instance.OnGamePaused += OnGamePaused;
        GameManager.Instance.OnGameUnPaused += OnGameUnPaused;
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
        if (drainPaused)
            return;

        float healAmount = 0;
        CurrentHunger -= StarveSpeed * Time.deltaTime;
        if (health.CurrentHealth < health.MaxHealth)
        {
            healAmount -= HealWithHungerSpeed * Time.deltaTime;
            CurrentHunger -= StarveWhileHealing * Time.deltaTime;
        }

        CurrentHunger = Mathf.Clamp(CurrentHunger, 0, MaxHunger);

        if (CurrentHunger == 0)
        {
            healAmount += DamageWithHungerSpeed * Time.deltaTime;
        }

        health.DealDamageWithoutScratch(healAmount);
    }

    public void EatFood(float amount)
    {
        CurrentHunger += amount;
        TookFood?.Invoke();
    }
}
