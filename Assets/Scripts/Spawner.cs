using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private Transform _leftFurtherSpawnPosition;
    [SerializeField] private Transform _rightNearSpawnPosition;
    [SerializeField] private float _spawnRate;
    [SerializeField] private int _poolCapacity = 10;
    [SerializeField] private int _poolMaxSize = 10;

    private ObjectPool<GameObject> _cubePool;
    private Coroutine _coroutine;
    private WaitForSeconds _spawnDelay;

    private void Awake()
    {
        _cubePool = new ObjectPool<GameObject>(
        createFunc: () => CreateCube(),
        actionOnGet: (cube) => ActionGet(cube),
        actionOnRelease: (cube) => cube.SetActive(false),
        actionOnDestroy: (cube) => Destroy(cube),
        collectionCheck: true,
        defaultCapacity: _poolCapacity,
        maxSize: _poolMaxSize);
    }

    private void Start()
    {
        _spawnDelay = new WaitForSeconds(_spawnRate);
        Coroutine spawnCubeCorutine = StartCoroutine(SpawnCube());
    }

    private GameObject CreateCube()
    {
        Cube newCube = Instantiate(_cubePrefab);
        return newCube.gameObject;
    }

    private void ActionGet(GameObject cube)
    {
        float xPosition = Random.Range(_leftFurtherSpawnPosition.position.x, _rightNearSpawnPosition.position.x);
        float zPosition = Random.Range(_rightNearSpawnPosition.position.z, _leftFurtherSpawnPosition.position.z);
        float yPosition = gameObject.transform.position.y;
        cube.transform.position = new Vector3(xPosition, yPosition, zPosition);
        cube.SetActive(true);
    }

    public void ReleaseCube(GameObject cube)
    {
            _cubePool.Release(cube);
    }

    private IEnumerator SpawnCube()
    {
        while (true)
        {    
            _cubePool.Get();
            yield return _spawnDelay;
        }
    }
}
