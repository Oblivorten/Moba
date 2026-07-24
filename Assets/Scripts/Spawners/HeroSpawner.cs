using UnityEngine;

public class HeroSpawner : MonoBehaviour
{
    [SerializeField] private Character _heroPrefab;
    [SerializeField] private Transform _spawnPoint;

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

        _currentHero = Instantiate(
            _heroPrefab,
            _spawnPoint.position,
            _spawnPoint.rotation);

        return _currentHero;
    }

    public void RespawnHero()
    {
        if (_currentHero != null)
        {
            Destroy(_currentHero.gameObject);
        }

        _currentHero = Instantiate(
            _heroPrefab,
            _spawnPoint.position,
            _spawnPoint.rotation);
    }
}