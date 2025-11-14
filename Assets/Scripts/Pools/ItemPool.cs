using System;
using UnityEngine;
using UnityEngine.Pool;

namespace Pools
{
    public class ItemPool<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] private T _prefab;
        [SerializeField] private int _defaultCapacity = 20;
        [SerializeField] private int _maxSize = 100;

        public event Action<int> TotalCreatedObjectCountChanged;
        public event Action<int> TotalSpawnedObjectCountChanged;
        public event Action<int> TotalActiveObjectCountChanged;

        private int _totalCreatedObjectCount;
        private int _totalSpawnedObjectCount;
        private int _totalActiveObjectCount;

        private ObjectPool<T> _pool;

        private void Awake()
        {
            if (_defaultCapacity > _maxSize)
            {
                throw new ArgumentOutOfRangeException("Размер по умолчанию превышает максимально допустимый размер");
            }

            _pool = new ObjectPool<T>(
                createFunc: Create,
                actionOnGet: GetItem,
                actionOnRelease: ReleaseItem,
                actionOnDestroy: DestroyItem,
                collectionCheck: true,
                defaultCapacity: _defaultCapacity,
                maxSize: _maxSize
            );
        }

        public T Get()
        {
            return _pool.Get();
        }

        public virtual void Release(T item)
        {
            _pool.Release(item);
        }

        private T Create()
        {
            T item = Instantiate(_prefab, transform);
            item.gameObject.SetActive(false);

            _totalCreatedObjectCount++;
            TotalCreatedObjectCountChanged?.Invoke(_totalCreatedObjectCount);
            
            return item;
        }

        private void GetItem(T item)
        {
            item.gameObject.SetActive(true);

            _totalSpawnedObjectCount++;
            _totalActiveObjectCount++;
            
            TotalSpawnedObjectCountChanged?.Invoke(_totalSpawnedObjectCount);
            TotalActiveObjectCountChanged?.Invoke(_totalActiveObjectCount);
        }

        private void ReleaseItem(T item)
        {
            item.gameObject.SetActive(false);
            
            _totalActiveObjectCount--;
            TotalActiveObjectCountChanged?.Invoke(_totalActiveObjectCount);
        }

        private void DestroyItem(T item)
        {
            Destroy(item.gameObject);
        }
    }
}