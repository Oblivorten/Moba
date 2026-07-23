using UnityEngine;

public class Creep : Character
{
    [SerializeField] private Transform _targetPoint;

    private void Update()
    {
        HandleMovement();
        HandleAttack();
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
            Movement.MoveTo(target.transform.position);
        }
    }
}