using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    [SerializeField] protected float _cooldown = 1f;
    [SerializeField] private string _abilityName;
    private float _lastUseTime;

    public string AbilityName => _abilityName;
    public float Cooldown => _cooldown;

    public bool IsReady => Time.time >= _lastUseTime + _cooldown;

    public bool TryUse()
    {
        if (!IsReady)
        {
            return false;
        }

        Use();
        _lastUseTime = Time.time;
        return true;
    }

    protected abstract void Use();
}