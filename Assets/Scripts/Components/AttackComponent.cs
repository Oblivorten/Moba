using UnityEngine;

public class AttackComponent : MonoBehaviour
{
    [SerializeField] private int _damage = 10;
    [SerializeField] private float _range = 2f;
    [SerializeField] private float _cooldown = 1f;

    private float _lastAttackTime;

    public float Range => _range;
    public int Damage => _damage;

    public bool CanAttack(GameObject target)
    {
        if (target == null) {
            return false;
        }

        if (Time.time < _lastAttackTime + _cooldown) {
            return false;
        }

        float distance = Vector3.Distance(transform.position, target.transform.position);
        return distance <= _range;
    }

    public bool TryAttack(GameObject target)
    {
        if (!CanAttack(target))
        {
            return false;
        }

        if (target.TryGetComponent<HealthComponent>(out var health))
        {
            health.TakeDamage(_damage);
            GetComponentInChildren<CharacterAnimator>()?.PlayAttack();
            _lastAttackTime = Time.time;
            return true;
        }

        return false;
    }

    public Vector3 GetChasePosition(Vector3 attackerPosition, Vector3 targetPosition)
    {
        Vector3 direction = (attackerPosition - targetPosition).normalized;

        if (direction == Vector3.zero)
        {
            direction = Vector3.forward;
        }

        return targetPosition + direction * (_range * 0.9f);
    }
}