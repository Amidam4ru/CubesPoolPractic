using System.Collections;
using System.Collections.Generic;
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

    private ObjectPool<Cube> _cubePool;
    private Coroutine _spawnCubeCorutine;
    private WaitForSeconds _spawnDelay;
    private List<Cube> _allSignedCubes;

    private void Awake()
    {
        _cubePool = new ObjectPool<Cube>(
        createFunc: () => CreateCube(),
        actionOnGet: (cube) => LaunchCube(cube),
        actionOnRelease: (cube) => cube.gameObject.SetActive(false),
        actionOnDestroy: (cube) => Destroy(cube.gameObject),
        collectionCheck: true,
        defaultCapacity: _poolCapacity,
        maxSize: _poolMaxSize);
    }

    private void OnEnable()
    {
        _allSignedCubes = new List<Cube>();
        _spawnDelay = new WaitForSeconds(_spawnRate);
        _spawnCubeCorutine = StartCoroutine(SpawnCube());
    }

    private void OnDisable()
    {
        StopCoroutine(_spawnCubeCorutine);
        UnsubscribeFromCubes();
    }

    private Cube CreateCube()
    {
        Cube newCube = Instantiate(_cubePrefab);
        newCube.Died += ReleaseCube;
        _allSignedCubes.Add(newCube);

        return newCube;
    }

    private void UnsubscribeFromCubes()
    {
        foreach (Cube cube in _allSignedCubes)
        {
            cube.Died -= ReleaseCube;
        }
    }

    private void ReleaseCube(Cube cube)
    {
        _cubePool.Release(cube);
    }

    private void LaunchCube(Cube cube)
    {
        float xPosition = Random.Range(_leftFurtherSpawnPosition.position.x, _rightNearSpawnPosition.position.x);
        float zPosition = Random.Range(_rightNearSpawnPosition.position.z, _leftFurtherSpawnPosition.position.z);
        float yPosition = transform.position.y;
        cube.transform.position = new Vector3(xPosition, yPosition, zPosition);
        cube.gameObject.SetActive(true);
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
