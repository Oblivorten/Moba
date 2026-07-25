using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
[RequireComponent(typeof(MovementComponent))]
[RequireComponent(typeof(AttackComponent))]
[RequireComponent(typeof(TargetComponent))]
public abstract class Character : Entity
{
    public HealthComponent Health { get; private set; }
    public MovementComponent Movement { get; private set; }
    public AttackComponent Attack { get; private set; }
    public TargetComponent Target { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        Health = GetComponent<HealthComponent>();
        Movement = GetComponent<MovementComponent>();
        Attack = GetComponent<AttackComponent>();
        Target = GetComponent<TargetComponent>();

        Health.OnDeath += HandleDeath;
    }

    protected virtual void HandleDeath()
    {
        Destroy(gameObject);
    }
}