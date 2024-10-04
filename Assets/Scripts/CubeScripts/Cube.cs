using System;
using UnityEngine;

[RequireComponent(typeof(CalculationLifeTime))]
public class Cube : MonoBehaviour
{
    private CalculationLifeTime _lifeTime;

    public event Action<Cube> Died;

    private void Awake()
    {
        _lifeTime = GetComponent<CalculationLifeTime>();
    }

    private void OnEnable()
    {
        _lifeTime.Destroyed += Release;
    }

    private void OnDisable()
    {
        _lifeTime.Destroyed -= Release;
    }

    private void Release()
    {
        Died?.Invoke(this);
    }
}
