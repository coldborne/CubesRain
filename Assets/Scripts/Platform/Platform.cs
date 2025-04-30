using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private CubeDestroyer _cubeDestroyer;

    private void Awake()
    {
        if (_cubeDestroyer == null)
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
                _cubeDestroyer.Destroy(cube);
            }
        }
    }
}