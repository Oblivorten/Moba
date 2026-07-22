using UnityEngine;
using System;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;

    public int CurrentHealth { get; protected set; }
    public int MaxHealth => _maxHealth;

    public event Action OnDeath;
    public event Action<int> OnHealthChanged;

    private void Awake()
    {
        CurrentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 0)
        {
            return;
        }
        CurrentHealth -= damage;
        CurrentHealth = Mathf.Max(CurrentHealth, 0);
        OnHealthChanged?.Invoke(CurrentHealth);
        if (CurrentHealth <= 0)
        {
            OnDeath?.Invoke();
        }
    }

    public void Heal(int amount)
    {
        if (amount <= 0)
        {
            return;
        }
        CurrentHealth += amount;
        CurrentHealth = Mathf.Min(CurrentHealth, _maxHealth);
        OnHealthChanged?.Invoke(CurrentHealth);
    }


}
