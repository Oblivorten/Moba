using UnityEngine;

public class Creep : Character
{
    [SerializeField] private Transform _targetPoint;
    [SerializeField] private float _detectionRadius = 4f;
    [SerializeField] private LayerMask _enemyLayer;

    private void Update()
    {
        HandleTargetDetection();
        HandleMovement();
        HandleAttack();
    }

    private void HandleTargetDetection()
    {
        if (Target.HasValidTarget)
        {
            return;
        }

        Entity enemy = FindNearestEnemy();

        if (enemy != null)
        {
            Target.SetTarget(enemy);
        }
    }

    private Entity FindNearestEnemy()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _detectionRadius, _enemyLayer);

        Entity nearest = null;
        float nearestDistance = float.MaxValue;

        foreach (Collider hit in hits)
        {
            if (!hit.TryGetComponent<Entity>(out var entity))
            {
                continue;
            }

            if (entity.Team == Team)
            {
                continue;
            }

            float distance = Vector3.Distance(transform.position, entity.transform.position);

            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearest = entity;
            }
        }

        return nearest;
    }

    private void HandleMovement()
    {
        if (Target.HasValidTarget)
        {
            return;
        }

        if (_targetPoint != null)
        {
            Movement.MoveTo(_targetPoint.position);
        }
    }

    private void HandleAttack()
    {
        if (!Target.HasValidTarget)
        {
            return;
        }

        Entity target = Target.CurrentTarget;

        if (Attack.CanAttack(target.gameObject))
        {
            Movement.Stop();
            Attack.TryAttack(target.gameObject);
        }
        else
        {
            Vector3 chasePos = Attack.GetChasePosition(transform.position, target.transform.position);
            Movement.MoveTo(chasePos);
        }
    }
}