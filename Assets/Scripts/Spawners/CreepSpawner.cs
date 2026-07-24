using UnityEngine;

public class CreepSpawner : MonoBehaviour
{
    [SerializeField] private Creep _creepPrefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _spawnInterval = 30f;
    [SerializeField] private int _creepsPerWave = 3;

    private float _nextSpawnTime;

    private void Start()
    {
        SpawnWave();
        _nextSpawnTime = Time.time + _spawnInterval;
    }

    private void Update()
    {
        if (Time.time >= _nextSpawnTime)
        {
            SpawnWave();
            _nextSpawnTime = Time.time + _spawnInterval;
        }
    }

    private void SpawnWave()
    {
        for (int i = 0; i < _creepsPerWave; i++)
        {
            Vector3 position = _spawnPoint.position + Vector3.right * i;

            Instantiate(
                _creepPrefab,
                position,
                Quaternion.identity);
        }
    }
}