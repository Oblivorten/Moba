using UnityEngine;

public class DashAbility : Ability
{
    [SerializeField] private float _dashDistance = 5f;
    [SerializeField] private LayerMask _groundLayer;

    private Camera _camera;
    private MovementComponent _movement;

    private void Awake()
    {
        _camera = Camera.main;
        _movement = GetComponent<MovementComponent>();
    }

    protected override void Use()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hit, 100f, _groundLayer))
        {
            Vector3 direction = (hit.point - transform.position).normalized;
            Vector3 destination = transform.position + direction * _dashDistance;
            _movement.Warp(destination);
        }
    }
}