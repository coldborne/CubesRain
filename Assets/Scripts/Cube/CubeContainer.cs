using System;
using UnityEngine;
using UnityEngine.Pool;

public class CubeContainer : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private int _defaultCapacity = 20;
    [SerializeField] private int _maxSize = 100;

    private ObjectPool<Cube> _pool;

    private void Awake()
    {
        if (_defaultCapacity > _maxSize)
        {
            throw new ArgumentOutOfRangeException("Размер по умолчанию превышает максимально допустимый размер");
        }

        _pool = new ObjectPool<Cube>(
            createFunc: CreateCube,
            actionOnGet: OnGetCube,
            actionOnRelease: OnReleaseCube,
            actionOnDestroy: OnDestroyCube,
            collectionCheck: true,
            defaultCapacity: _defaultCapacity,
            maxSize: _maxSize
        );
    }

    private Cube CreateCube()
    {
        Cube cube = Instantiate(_cubePrefab, transform);
        cube.gameObject.SetActive(false);
        return cube;
    }

    private void OnGetCube(Cube cube)
    {
        cube.gameObject.SetActive(true);
    }

    private void OnReleaseCube(Cube cube)
    {
        cube.gameObject.SetActive(false);
    }

    private void OnDestroyCube(Cube cube)
    {
        Destroy(cube.gameObject);
    }

    public Cube GetCube()
    {
        return _pool.Get();
    }

    public void ReleaseCube(Cube cube)
    {
        cube.UnTouch();
        _pool.Release(cube);
    }
}