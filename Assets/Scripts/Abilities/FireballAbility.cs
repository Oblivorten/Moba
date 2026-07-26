using UnityEngine;

public class FireballAbility : Ability
{
    [SerializeField] private int _damage = 30;
    [SerializeField] private float _radius = 3f;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private GameObject _vfxPrefab;

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

        if (_vfxPrefab != null)
        {
            GameObject vfx = Instantiate(_vfxPrefab, center, Quaternion.identity);
            Destroy(vfx, 2f);
        }

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