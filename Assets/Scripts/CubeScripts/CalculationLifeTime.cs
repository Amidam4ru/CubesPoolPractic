using System.Collections;
using UnityEngine;

public class CalculationLifeTime : MonoBehaviour
{
    [SerializeField, Min(1)] private float _minLifeTime = 2f;
    [SerializeField] private float _maxLifeTime = 6f;

    private float _lifeTime;
    private WaitForSeconds _releaseDeleay;
    private Coroutine _releaseCorutine;
    private Spawner _spawner;

    public void CalculateLifeTime()
    {
        _lifeTime = Random.Range(_minLifeTime, _maxLifeTime);
        _releaseDeleay = new WaitForSeconds(_lifeTime);
        _releaseCorutine = StartCoroutine(Release());
    }

    private IEnumerator Release()
    {
        yield return _releaseDeleay;

        _spawner = FindObjectOfType<Spawner>();

        if (_spawner != null)
        {
            _spawner.ReleaseCube(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnValidate()
    {
        if (_minLifeTime > _maxLifeTime)
        {
            _maxLifeTime = _minLifeTime + 1;
        }
    }
}
