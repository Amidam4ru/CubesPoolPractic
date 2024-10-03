using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Cube))]
public class CalculationLifeTime : MonoBehaviour
{
    [SerializeField, Min(1)] private float _minLifeTime = 2f;
    [SerializeField] private float _maxLifeTime = 6f;

    private float _lifeTime;
    private WaitForSeconds _releaseDeleay;
    private Coroutine _releaseCorutine;
    private Spawner _spawner;

    public event Action<Cube> Lived;

    private void OnValidate()
    {
        if (_minLifeTime > _maxLifeTime)
        {
            _maxLifeTime = _minLifeTime + 1;
        }
    }

    public void CalculateLifeTime()
    {
        _lifeTime = UnityEngine.Random.Range(_minLifeTime, _maxLifeTime);
        _releaseDeleay = new WaitForSeconds(_lifeTime);
        _releaseCorutine = StartCoroutine(Release());
    }

    private IEnumerator Release()
    {
        yield return _releaseDeleay;

        Lived?.Invoke(gameObject.GetComponent<Cube>());
    }
}
