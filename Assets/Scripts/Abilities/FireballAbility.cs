using UnityEngine;

public class FireballAbility : Ability
{
    [SerializeField] private int _damage = 30;
    [SerializeField] private float _radius = 3f;
    [SerializeField] private LayerMask _enemyLayer;

    private Entity _owner;

    private void Awake()
    {
        _owner = GetComponent<Entity>();
    }

    protected override void Use()
    {
        TargetComponent target = GetComponent<TargetComponent>();

        if (!target.HasValidTarget)
        {
            return;
        }

        Vector3 center = target.CurrentTarget.transform.position;
        Collider[] hits = Physics.OverlapSphere(center, _radius, _enemyLayer);

        foreach (Collider hit in hits)
        {
            if (!hit.TryGetComponent<Entity>(out var entity))
            {
                continue;
            }

            if (entity.Team == _owner.Team)
            {
                continue;
            }

            if (entity.TryGetComponent<HealthComponent>(out var health))
            {
                health.TakeDamage(_damage);
            }
        }
    }
}