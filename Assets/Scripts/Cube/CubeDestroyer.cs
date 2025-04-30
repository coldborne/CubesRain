using System.Collections;
using UnityEngine;

public class CubeDestroyer : MonoBehaviour
{
    [SerializeField] private CubeContainer _cubeContainer;

    private int _minLifeTime;
    private int _maxLifeTime;

    private void Awake()
    {
        if (_cubeContainer == null)
        {
            throw new UnityException("Нет ссылки на пул кубов для создания кубов");
        }

        _minLifeTime = 2;
        _maxLifeTime = 5;
    }

    public void Destroy(Cube cube)
    {
        int cubeLifeTime = Random.Range(_minLifeTime, _maxLifeTime);
        StartCoroutine(DestroyCubeAfterDelay(cube, cubeLifeTime));
    }

    private IEnumerator DestroyCubeAfterDelay(Cube cube, int delay)
    {
        yield return new WaitForSeconds(delay);

        _cubeContainer.ReleaseCube(cube);
    }
}