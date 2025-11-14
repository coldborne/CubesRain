using interfaces;
using Pools;
using UI;
using UnityEngine;

namespace Spawners
{
    public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour, IExpirable<T>
    {
        [SerializeField] private SpawnerView _spawnerView;
        [SerializeField] private ItemPool<T> _items;

        private void Awake()
        {
            if (_items == null)
            {
                throw new UnityException("Нет ссылки на пул кубов для создания кубов");
            }
        }

        private void OnEnable()
        {
            _items.TotalSpawnedObjectCountChanged += ShowTotalSpawnedObjectCount;
            _items.TotalCreatedObjectCountChanged += ShowTotalCreatedObjectCount;
            _items.TotalActiveObjectCountChanged += ShowActiveObjectCount;
        }

        private void OnDisable()
        {
            _items.TotalSpawnedObjectCountChanged -= ShowTotalSpawnedObjectCount;
            _items.TotalCreatedObjectCountChanged -= ShowTotalCreatedObjectCount;
            _items.TotalActiveObjectCountChanged -= ShowActiveObjectCount;
        }

        public void Spawn(Vector3 position)
        {
            T item = _items.Get();

            item.transform.position = position;
            item.gameObject.SetActive(true);

            item.Expired += Release;
        }

        protected virtual void Release(T item)
        {
            item.Expired -= Release;
            _items.Release(item);
        }

        private void ShowTotalSpawnedObjectCount(int count)
        {
            _spawnerView.ShowTotalSpawnedObjectCount(count);
        }

        private void ShowTotalCreatedObjectCount(int count)
        {
            _spawnerView.ShowTotalCreatedObjectCount(count);
        }

        private void ShowActiveObjectCount(int count)
        {
            _spawnerView.ShowActiveObjectCount(count);
        }
    }
}