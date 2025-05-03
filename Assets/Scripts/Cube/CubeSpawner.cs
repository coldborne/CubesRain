using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private CubePool _cubes;

    private void Awake()
    {
        if (_cubes == null)
        {
            throw new UnityException("Нет ссылки на пул кубов для создания кубов");
        }
    }
    
    public void Spawn(Vector3 position)
    {
        Cube cube = _cubes.GetCube();
        
        cube.transform.position = position;
        cube.gameObject.SetActive(true);

        cube.Expired += Release;
    }
    
    public void Release(Cube cube)
    {
        cube.Expired -= Release;
        _cubes.ReleaseCube(cube);
    }
}