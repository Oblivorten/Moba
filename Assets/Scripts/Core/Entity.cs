using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField] private Team team;
    [SerializeField] private int maxHealth = 100;

    public int CurrentHealth { get; protected set; }
    public Team Team => team;

    protected virtual void Awake()
    {
        CurrentHealth = maxHealth;
    }

    public virtual void TakeDamage(int damage)
    {
        if (damage <= 0) {
            return;
        }

        CurrentHealth -= damage;
        CurrentHealth = Mathf.Max(CurrentHealth, 0);

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}