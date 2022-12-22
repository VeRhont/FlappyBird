using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _obstaclePatterns;
    [SerializeField] private GameObject _collectable;

    [SerializeField] private Transform _obstaclesAnchor;
    [SerializeField] private Vector2 _spawnPoint;

    [SerializeField] private float _firstSpawn;
    [SerializeField] private float _timeBetweenSpawn;
    [SerializeField] private float _minTimeBetweenSpawn;

    [SerializeField, Range(0, 1)] private float _spawnCollectableChance;

    private void Start()
    {
        Invoke("SpawnRandomObstacle", _firstSpawn);
        Invoke("SpawnCollectable", _firstSpawn);
    }

    private void Update()
    {
        if (_timeBetweenSpawn > _minTimeBetweenSpawn)
        {
            _timeBetweenSpawn -= (Time.deltaTime / 30f);
        }
    }

    private void SpawnRandomObstacle()
    {
        var randomIndex = Random.Range(0, _obstaclePatterns.Length);

        var obstacle = Instantiate(_obstaclePatterns[randomIndex], _spawnPoint, Quaternion.identity);
        obstacle.transform.parent = _obstaclesAnchor;

        foreach (Transform obstacleParent in _obstaclesAnchor)
        {
            if (obstacleParent.childCount == 0)
            {
                Destroy(obstacleParent.gameObject);
            }
        }

        Invoke("SpawnRandomObstacle", _timeBetweenSpawn);
    }

    private void SpawnCollectable()
    {
        if (Random.value <= _spawnCollectableChance)
        {
            var position = new Vector2(_spawnPoint.x + Random.Range(-3f, 3f), Random.Range(-4f, 4f));
            Instantiate(_collectable, position, Quaternion.identity);
        }

        Invoke("SpawnCollectable", _timeBetweenSpawn);
    }
}
