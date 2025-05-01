using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private CubeSpawner _cubeSpawner;

    private void Awake()
    {
        if (_cubeSpawner == null)
        {
            throw new UnityException("Нет ссылки на уничтожителя кубов для уничтожения кубов");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out Cube cube))
        {
            if (cube.HasCollidedWithPlatform == false)
            {
                cube.Touch();
            }
        }
    }
}