using System.Collections;
using UnityEngine;

public class HeroSpawner : MonoBehaviour
{
    [SerializeField] private Character _heroPrefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _respawnDelay = 5f;

    private Character _currentHero;

    private void Start()
    {
        SpawnHero();
    }

    public Character SpawnHero()
    {
        if (_currentHero != null)
        {
            return _currentHero;
        }

        _currentHero = Instantiate(_heroPrefab, _spawnPoint.position, _spawnPoint.rotation);
        _currentHero.Health.OnDeath += HandleHeroDeath;

        return _currentHero;
    }

    private void HandleHeroDeath()
    {
        StartCoroutine(RespawnAfterDelay());
    }

    private IEnumerator RespawnAfterDelay()
    {
        yield return new WaitForSeconds(_respawnDelay);
        RespawnHero();
    }

    public void RespawnHero()
    {
        if (_currentHero != null)
        {
            _currentHero.Health.OnDeath -= HandleHeroDeath;
            Destroy(_currentHero.gameObject);
        }

        _currentHero = Instantiate(_heroPrefab, _spawnPoint.position, _spawnPoint.rotation);
        _currentHero.Health.OnDeath += HandleHeroDeath;
    }
}