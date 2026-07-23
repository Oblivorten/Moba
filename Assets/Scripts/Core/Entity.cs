using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField] private Team _team;

    public Team Team => _team;

    protected virtual void Awake()
    {

    }
}