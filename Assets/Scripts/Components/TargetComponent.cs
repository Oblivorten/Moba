using UnityEngine;
using System;

public class TargetComponent : MonoBehaviour
{
    public Entity CurrentTarget { get; private set; }

    public event Action<Entity> OnTargetChanged;
    public event Action OnTargetLost;

    public bool HasValidTarget
    {
        get
        {
            if (CurrentTarget == null) {
                return false;
            }

            if (CurrentTarget.TryGetComponent<HealthComponent>(out var health))
            {
                if (health.CurrentHealth <= 0)
                {
                    ClearTarget();
                    return false;
                }
            }

            return true;
        }
    }

    public void SetTarget(Entity target)
    {
        if (target == CurrentTarget) {
            return;
        }

        CurrentTarget = target;
        OnTargetChanged?.Invoke(CurrentTarget);
    }

    public void ClearTarget()
    {
        if (CurrentTarget == null) {
            return;
        }

        CurrentTarget = null;
        OnTargetLost?.Invoke();
        OnTargetChanged?.Invoke(null);
    }

    private void Update()
    {
        if (CurrentTarget != null && !HasValidTarget)
        {
            ClearTarget();
        }
    }
}