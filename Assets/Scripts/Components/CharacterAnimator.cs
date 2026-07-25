using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private NavMeshAgent _agent;

    private void Update()
    {
        _animator.SetFloat("Speed", _agent.velocity.magnitude);
    }

    public void PlayAttack()
    {
        _animator.SetTrigger("Attack");
    }

    public void PlayDeath()
    {
        _animator.SetTrigger("Die");
    }
}