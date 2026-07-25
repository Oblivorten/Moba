using UnityEngine;

[RequireComponent(typeof(EnemyHero))]
public class EnemyHeroAI : MonoBehaviour
{
    [SerializeField] private Transform _targetPoint;
    [SerializeField] private float _detectionRadius = 5f;
    [SerializeField] private float _leashRadius = 7f;
    [SerializeField] private LayerMask _enemyLayer;

    private EnemyHero _hero;

    private void Awake()
    {
        _hero = GetComponent<EnemyHero>();
    }

    private void Update()
    {
        HandleTargetDetection();
        HandleLeash();

        if (_hero.Target.HasValidTarget)
        {
            AttackTarget();
        }
        else
        {
            MoveAlongLane();
        }
    }

    private void HandleTargetDetection()
    {
        if (_hero.Target.HasValidTarget)
        {
            return;
        }

        Entity enemy = FindNearestEnemy();

        if (enemy != null)
        {
            _hero.Target.SetTarget(enemy);
        }
    }

    private void HandleLeash()
    {
        if (!_hero.Target.HasValidTarget)
        {
            return;
        }

        Entity target = _hero.Target.CurrentTarget;
        float distance = Vector3.Distance(transform.position, target.transform.position);

        if (distance > _leashRadius)
        {
            _hero.Target.ClearTarget();
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

            if (entity.Team == _hero.Team)
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

    private void MoveAlongLane()
    {
        _hero.Movement.MoveTo(_targetPoint.position);
    }

    private void AttackTarget()
    {
        Entity target = _hero.Target.CurrentTarget;

        if (_hero.Attack.TryAttack(target.gameObject))
        {
            _hero.Movement.Stop();
        }
        else
        {
            _hero.Movement.MoveTo(target.transform.position);
        }
    }
}