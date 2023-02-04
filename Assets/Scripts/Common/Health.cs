using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float MaxHealth = 100;
    public float CurrentHealth;
    
    public event System.Action IsDying;
    public event System.Action TookDamage;
    public event System.Action Healed;

    private void Awake()
    {
        CurrentHealth = MaxHealth;
    }
    public float GetNormalizedHealth()
    {
        return Mathf.Round(CurrentHealth);
    }

    public void DealDamage(float amount)
    {
        CurrentHealth -= amount;
        
        if (CurrentHealth >= MaxHealth)
            CurrentHealth = MaxHealth;

        if (CurrentHealth <= 0)
            IsDying?.Invoke();
        else if (amount > 0)
            TookDamage?.Invoke();
        else if (amount < 0)
            Healed?.Invoke();
    }

}
