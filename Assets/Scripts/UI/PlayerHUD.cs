using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private Image _healthFill;
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private Image _qCooldownFill;
    [SerializeField] private Image _wCooldownFill;
    [SerializeField] private Image _eCooldownFill;

    private PlayerHero _player;
    private HealthComponent _health;
    private Ability _q, _w, _e;

    private void Update()
    {
        if (_player == null)
        {
            if (PlayerHero.Instance == null)
            {
                return;
            }

            _player = PlayerHero.Instance;
            _health = _player.Health;
            _health.OnHealthChanged += UpdateHealth;
            UpdateHealth(_health.CurrentHealth);

            var abilities = _player.GetComponents<Ability>();
            if (abilities.Length > 0) _q = abilities[0];
            if (abilities.Length > 1) _w = abilities[1];
            if (abilities.Length > 2) _e = abilities[2];
        }

        UpdateCooldown(_q, _qCooldownFill);
        UpdateCooldown(_w, _wCooldownFill);
        UpdateCooldown(_e, _eCooldownFill);
    }

    private void UpdateHealth(int current)
    {
        _healthFill.fillAmount = (float)current / _health.MaxHealth;
        _healthText.text = $"{current} / {_health.MaxHealth}";
    }

    private void UpdateCooldown(Ability ability, Image fill)
    {
        if (ability == null || fill == null) return;
        fill.fillAmount = ability.Cooldown > 0 ? ability.CooldownRemaining / ability.Cooldown : 0;
    }
}