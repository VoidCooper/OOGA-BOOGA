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

    public event System.Action TookFood;

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

    public void Update()
    {
        CurrentHunger -= StarveSpeed * Time.deltaTime;
        if (health.CurrentHealth < health.MaxHealth)
        {
            CurrentHunger += HealWithHungerSpeed * Time.deltaTime;
            CurrentHunger -= StarveWhileHealing * Time.deltaTime;
        }
    }

    public void EatFood()
    {
        TookFood?.Invoke();
    }
}
