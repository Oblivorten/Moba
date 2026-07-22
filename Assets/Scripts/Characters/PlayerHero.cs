using UnityEngine;

public class PlayerHero : Character
{
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private float _raycastDistance = 100f;

    private Camera _mainCamera;
    private AbilityComponent _abilityComponent;

    protected override void Awake()
    {
        base.Awake();
        _mainCamera = Camera.main;
        _abilityComponent = GetComponent<AbilityComponent>();
    }

    private void Update()
    {
        HandleInput();
        HandleAutoAttack();
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ProcessRightClick();
        }
        if (Input.GetKeyDown(KeyCode.Q)) {
            _abilityComponent?.UseQ();
        }
        if (Input.GetKeyDown(KeyCode.W)) {
            _abilityComponent?.UseW();
        }
        if (Input.GetKeyDown(KeyCode.E)) {
            _abilityComponent?.UseE();
        }
    }

    private void ProcessRightClick()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var targetHit, _raycastDistance, _targetLayer))
        {
            if (targetHit.collider.TryGetComponent<Entity>(out var targetEntity))
            {
                if (targetEntity != this)
                {
                    Target.SetTarget(targetEntity);
                    return;
                }
            }
        }

        if (Physics.Raycast(ray, out var groundHit, _raycastDistance, _groundLayer))
        {
            Target.ClearTarget();
            Movement.SetDestination(groundHit.point);
        }
    }

    private void HandleAutoAttack()
    {
        if (!Target.HasValidTarget) {
            return;
        }

        GameObject targetGo = Target.CurrentTarget.gameObject;

        if (Attack.CanAttack(targetGo))
        {
            Movement.Stop();
            Attack.TryAttack(targetGo);
        }
        else
        {
            Movement.SetDestination(targetGo.transform.position);
        }
    }
}