using UnityEngine;

public class AbilitySystem : MonoBehaviour
{
    [SerializeField] private Ability _abilityQ;
    [SerializeField] private Ability _abilityW;
    [SerializeField] private Ability _abilityE;

    public void UseQ()
    {
        _abilityQ?.TryUse();
    }

    public void UseW()
    {
        _abilityW?.TryUse();
    }

    public void UseE()
    {
        _abilityE?.TryUse();
    }
}