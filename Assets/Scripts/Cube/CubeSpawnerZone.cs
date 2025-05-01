using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class CubeSpawnerZone : MonoBehaviour
{
    [SerializeField] private CubeSpawner _cubeSpawner;

    [SerializeField] private bool _hasSpawnStopped;

    private BoxCollider _boxCollider;

    private int _delay;

    private void Awake()
    {
        if (_cubeSpawner == null)
        {
            throw new UnityException("Нет ссылки на создателя кубов для спавна кубов");
        }

        _boxCollider = GetComponent<BoxCollider>();

        _delay = 1;
    }

    private void Start()
    {
        StartCoroutine(SpawnNext());
    }

    private IEnumerator SpawnNext()
    {
        while (_hasSpawnStopped == false)
        {
            Spawn();

            yield return new WaitForSeconds(_delay);
        }
    }

    private void Spawn()
    {
        Bounds zoneBounds = _boxCollider.bounds;

        float minX = zoneBounds.min.x;
        float maxX = zoneBounds.max.x;

        float minZ = zoneBounds.min.z;
        float maxZ = zoneBounds.max.z;

        float x = Random.Range(minX, maxX);
        float z = Random.Range(minZ, maxZ);

        Vector3 position = new Vector3(x, zoneBounds.min.y, z);

        _cubeSpawner.Spawn(position);
    }
}