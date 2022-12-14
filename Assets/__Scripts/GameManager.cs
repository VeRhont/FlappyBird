using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _tree;
    [SerializeField] private GameObject _collectable;

    [SerializeField] private Transform _treeAnchor;
    [SerializeField] private float _spawnPoint;

    [SerializeField] private float _firstSpawn;
    [SerializeField] private float _timeBetweenSpawn;
    [SerializeField] private float _minTimeBetweenSpawn;

    [SerializeField, Range(0, 1)] private float _spawnCollectableChance;

    private void Start()
    {
        Invoke("SpawnTree", _firstSpawn);
    }

    private void Update()
    {
        if (_timeBetweenSpawn > _minTimeBetweenSpawn)
        {
            _timeBetweenSpawn -= (Time.deltaTime / 130f);
        }
    }

    private void SpawnTree()
    {
        if (Random.value < _spawnCollectableChance) SpawnCollectable();

        var y = Random.Range(-2.5f, 2.5f);
        var position = new Vector2(_spawnPoint, y);

        var tree = Instantiate(_tree, position, Quaternion.identity);
        tree.transform.parent = _treeAnchor;

        Invoke("SpawnTree", _timeBetweenSpawn);
    }

    private void SpawnCollectable()
    {
        var position = new Vector2(_spawnPoint + 5f, Random.Range(-4f, 4f));
        Instantiate(_collectable, position, Quaternion.identity);
    }
}
