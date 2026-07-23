using UnityEngine;

[RequireComponent(typeof(EnemyHero))]
public class EnemyHeroAI : MonoBehaviour
{
    [SerializeField] private Transform _targetPoint;
    private EnemyHero _hero;

    private void Awake()
    {
        _hero = GetComponent<EnemyHero>();
    }

    private void Update()
    {
        if (_hero.Target.HasValidTarget)
        {
            AttackTarget();
        }
        else
        {
            MoveAlongLane();
        }
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