using UnityEngine;

public class GameZoneSpawner : MonoBehaviour
{
    [SerializeField] private GameZoneCreator _gameZoneCreator;

    private void Awake()
    {
        if ( _gameZoneCreator == null)
        {
            throw new UnityException("Нет ссылки на создателя игровой зоны для спавна");
        }
    }

    public void Spawn(Vector3 position, Quaternion rotation)
    {
        GameObject gameZone = _gameZoneCreator.Create();

        gameZone.transform.position = position;
        gameZone.transform.rotation = rotation;

        gameZone.SetActive(true);
    }
}