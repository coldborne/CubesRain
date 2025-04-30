using UnityEngine;

public class CubeCreator : MonoBehaviour
{
    [SerializeField] private CubeContainer _cubeContainer;

    private void Awake()
    {
        if (_cubeContainer == null)
        {
            throw new UnityException("Нет ссылки на пул кубов для создания кубов");
        }
    }

    public Cube Create()
    {
        return _cubeContainer.GetCube();
    }
}