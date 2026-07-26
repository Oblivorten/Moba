using UnityEngine;
using UnityEngine.UI;

public class HealthBarWorld : MonoBehaviour
{
    [SerializeField] private Image _fillImage;
    [SerializeField] private Vector3 _offset = new Vector3(0, 2.5f, 0);

    private HealthComponent _health;
    private Transform _target;
    private Camera _camera;

    private void Awake()
    {
        _health = GetComponentInParent<HealthComponent>();
        _target = transform.parent;
        _camera = Camera.main;
    }

    private void Start()
    {
        if (_health == null) return;

        _health.OnHealthChanged += UpdateBar;
        UpdateBar(_health.CurrentHealth);
    }

    private void LateUpdate()
    {
        transform.position = _target.position + _offset;
        transform.rotation = _camera.transform.rotation;
    }

    private void UpdateBar(int currentHealth)
    {
        _fillImage.fillAmount = (float)currentHealth / _health.MaxHealth;
    }

    private void OnDestroy()
    {
        if (_health != null)
        {
            _health.OnHealthChanged -= UpdateBar;
        }
    }
}