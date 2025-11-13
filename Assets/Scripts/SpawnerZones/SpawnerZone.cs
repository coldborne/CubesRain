using System.Collections;
using interfaces;
using Spawners;
using UnityEngine;

namespace SpawnerZones
{
    public abstract class SpawnerZone<T> : MonoBehaviour where T : MonoBehaviour, IExpirable<T>
    {
        [SerializeField] private Spawner<T> _spawner;

        [SerializeField] private bool _hasSpawnStopped;

        private int _delay;

        private void Awake()
        {
            if (_spawner == null)
            {
                throw new UnityException("Нет ссылки на создателя кубов для спавна кубов");
            }

            _delay = 1;

            Init();
        }

        protected abstract void Init();

        private void Start()
        {
            StartCoroutine(SpawnNext());
        }

        private IEnumerator SpawnNext()
        {
            while (_hasSpawnStopped == false)
            {
                Spawn();

                yield return new WaitForSeconds(_delay);
            }
        }

        protected abstract Vector3 GetPosition();

        private void Spawn()
        {
            Vector3 position = GetPosition();
            _spawner.Spawn(position);
        }
    }
}