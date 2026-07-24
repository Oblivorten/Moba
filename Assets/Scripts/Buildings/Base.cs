using UnityEngine;

public class Base : Building
{
    [SerializeField] private float _healRadius = 8f;
    [SerializeField] private int _healAmount = 10;
    [SerializeField] private float _healInterval = 1f;

    private float _nextHealTime;

    private void Update()
    {
        if (Time.time >= _nextHealTime)
        {
            HealAllies();
            _nextHealTime = Time.time + _healInterval;
        }
    }

    private void HealAllies()
    {
        Collider[] colliders = Physics.OverlapSphere(
            transform.position,
            _healRadius);

        foreach (Collider collider in colliders)
        {
            if (!collider.TryGetComponent<Character>(out Character character))
            {
                continue;
            }

            if (character.Team != Team)
            {
                continue;
            }

            character.Health.Heal(_healAmount);
        }
    }

    protected override void OnDestroyed()
    {
        base.OnDestroyed();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _healRadius);
    }
}