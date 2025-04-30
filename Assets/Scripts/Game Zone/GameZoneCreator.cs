using UnityEngine;

public class GameZoneCreator : MonoBehaviour
{
    [SerializeField] private GameObject _gameZonePrefab;

    private void Awake()
    {
        if (_gameZonePrefab == null)
        {
            throw new UnityException("Не установлен префаб для создания игровой зоны");
        }
    }

    public GameObject Create()
    {
        GameObject gameZone = Instantiate(_gameZonePrefab);
        gameZone.SetActive(false);

        return gameZone;
    }
}