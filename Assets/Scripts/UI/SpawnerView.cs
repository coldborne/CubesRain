using TMPro;
using UnityEngine;

namespace UI
{
    public class SpawnerView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _allSpawnedObjectCountText;
        [SerializeField] private TextMeshProUGUI _allCreatedObjectCountText;
        [SerializeField] private TextMeshProUGUI _allActiveObjectCountText;
        [SerializeField] private TextMeshProUGUI _spawnerNameText;

        [SerializeField] private string _spawnerName;
        
        private void Awake()
        {
            ShowSpawnerName();
        }

        public void ShowTotalCreatedObjectCount(int totalCreatedObjectCount)
        {
            _allCreatedObjectCountText.text = $"Общее количество созданных объектов: {totalCreatedObjectCount}";
        }

        public void ShowTotalSpawnedObjectCount(int totalSpawnedObjectCount)
        {
            _allSpawnedObjectCountText.text = $"Общее количество заспавненных объектов: {totalSpawnedObjectCount}";
        }

        public void ShowActiveObjectCount(int totalActiveObjectCount)
        {
            _allActiveObjectCountText.text = $"Количество активных объектов: {totalActiveObjectCount}";
        }

        private void ShowSpawnerName()
        {
            _spawnerNameText.text = _spawnerName;
        }
    }
}