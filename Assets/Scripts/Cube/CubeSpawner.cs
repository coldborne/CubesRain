using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public void Spawn(Cube cube, Vector3 position)
    {
        cube.transform.position = position;
        cube.gameObject.SetActive(true);
    }
}