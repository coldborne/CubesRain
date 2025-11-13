using interfaces;
using Pools;
using UnityEngine;

namespace Spawners
{
    public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour, IExpirable<T>
    {
        [SerializeField] private ItemPool<T> _items;

        private void Awake()
        {
            if (_items == null)
            {
                throw new UnityException("Нет ссылки на пул кубов для создания кубов");
            }
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
    }
}