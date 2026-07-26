using UnityEngine;

public class PlayerHero : Character
{
    public static PlayerHero Instance { get; private set; }

    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private float _raycastDistance = 100f;

    private Camera _mainCamera;
    private AbilitySystem _abilitySystem;

    protected override void Awake()
    {
        base.Awake();
        Instance = this;
        _mainCamera = Camera.main;
        _abilitySystem = GetComponent<AbilitySystem>();
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
            _abilitySystem?.UseQ();
        }
        if (Input.GetKeyDown(KeyCode.W)) {
            _abilitySystem?.UseW();
        }
        if (Input.GetKeyDown(KeyCode.E)) {
            _abilitySystem?.UseE();
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
            Movement.MoveTo(groundHit.point);
        }
    }

    private void HandleAutoAttack()
    {
        if (!Target.HasValidTarget)
        {
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
            Vector3 chasePos = Attack.GetChasePosition(transform.position, targetGo.transform.position);
            Movement.MoveTo(chasePos);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}