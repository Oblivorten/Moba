using UnityEngine;

[RequireComponent(typeof(AttackComponent))]
public class Tower : Building
{
    [SerializeField] private LayerMask _enemyLayer;
    private AttackComponent _attack;


    protected override void Awake()
    {
        base.Awake();

        _attack = GetComponent<AttackComponent>();
    }


    private void Update()
    {
        Entity target = FindTarget();

        if (target == null)
        {
            return;
        }

        _attack.TryAttack(target.gameObject);
    }


    private Entity FindTarget()
    {
        Collider[] targets = Physics.OverlapSphere(
            transform.position,
            _attack.Range,
            _enemyLayer
        );


        foreach (Collider target in targets)
        {
            if (target.TryGetComponent<Entity>(out Entity entity))
            {
                if (entity.Team != Team)
                {
                    return entity;
                }
            }
        }


        return null;
    }


    private void OnDrawGizmosSelected()
    {
        if (TryGetComponent<AttackComponent>(out var attack))
        {
            Gizmos.DrawWireSphere(
                transform.position,
                attack.Range
            );
        }
    }
}