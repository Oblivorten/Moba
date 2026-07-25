using UnityEngine;

public class HealAbility : Ability
{
    [SerializeField] private int _healAmount = 40;

    private HealthComponent _health;

    private void Awake()
    {
        _health = GetComponent<HealthComponent>();
    }

    protected override void Use()
    {
        _health.Heal(_healAmount);
    }
}