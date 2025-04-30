using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private GameZoneSpawner _gameZoneSpawner;

    private void Awake()
    {
        if (_gameZoneSpawner == null)
        {
            throw new UnityException("Нет ссылки на спавнера игровой зоны для спавна");
        }

        SpawnGameZone();
    }

    private void SpawnGameZone()
    {
        Vector3 position = Vector3.zero;
        Quaternion rotation = Quaternion.identity;

        _gameZoneSpawner.Spawn(position, rotation);
    }
}
