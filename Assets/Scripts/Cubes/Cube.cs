using System;
using System.Collections;
using interfaces;
using Platforms;
using Pools;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Cubes
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Renderer))]
    public class Cube : TouchableObject, IExpirable<Cube>
    {
        [SerializeField] private Color[] _colors;
        [SerializeField] private Color _defaultColor;

        public event Action<Cube> Expired;

        private Renderer _renderer;

        public bool HasCollidedWithPlatform { get; private set; }

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
            _renderer.material.color = _defaultColor;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.TryGetComponent<Platform>(out _))
            {
                if (HasCollidedWithPlatform == false)
                {
                    Touch();
                }
            }
        }

        public override void Touch()
        {
            if (HasCollidedWithPlatform == false)
            {
                _renderer.material.color = _colors[Random.Range(0, _colors.Length)];
                HasCollidedWithPlatform = true;

                int minLifeTime = 2;
                int maxLifeTime = 5;

                int cubeLifeTime = Random.Range(minLifeTime, maxLifeTime + 1);
                StartCoroutine(ExpireAfterDelay(cubeLifeTime));
            }
        }

        public override void UnTouch()
        {
            if (HasCollidedWithPlatform)
            {
                _renderer.material.color = _defaultColor;
                HasCollidedWithPlatform = false;
            }
        }

        private IEnumerator ExpireAfterDelay(int delay)
        {
            yield return new WaitForSeconds(delay);

            Expire();
        }

        public void Expire()
        {
            Expired?.Invoke(this);
        }
    }
}