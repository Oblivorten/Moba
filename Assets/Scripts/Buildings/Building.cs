using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
public abstract class Building : Entity
{
    public HealthComponent Health { get; private set; }


    protected override void Awake()
    {
        base.Awake();

        Health = GetComponent<HealthComponent>();

        Health.OnDeath += OnDestroyed;
    }


    protected virtual void OnDestroyed()
    {
        Destroy(gameObject);
    }
}