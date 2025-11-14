using System;
using System.Collections;
using System.Collections.Generic;
using interfaces;
using Logic;
using UnityEngine;

namespace Bombs
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Renderer))]
    public class Bomb : MonoBehaviour, IExpirable<Bomb>
    {
        [SerializeField] private float _baseExplosionRadius;
        [SerializeField] private float _explosionForce;

        public event Action<Bomb> Expired;

        private readonly int _minDisappearanceTime = 2;
        private readonly int _maxDisappearanceTime = 5;

        private Renderer _renderer;
        private Color _defaultColor;

        private void Awake()
        {
            _defaultColor = Color.black;

            _renderer = GetComponent<Renderer>();
        }

        private void OnEnable()
        {
            _renderer.material.color = _defaultColor;
            Activate();
        }

        public void Expire()
        {
            Expired?.Invoke(this);
        }

        private int GenerateDisappearanceTime()
        {
            return RandomGenerator.Next(_minDisappearanceTime, _maxDisappearanceTime);
        }

        private void Activate()
        {
            int disappearanceTime = GenerateDisappearanceTime();
            StartCoroutine(Disappear(disappearanceTime));
        }

        private IEnumerator Disappear(int time)
        {
            Color color = _renderer.material.color;
            float startAlpha = color.a;
            float endAlpha = 0.0f;

            float elapsedTime = 0.0f;

            while (elapsedTime < time)
            {
                float normalizedTime = elapsedTime / time;

                float newAlpha = Mathf.Lerp(startAlpha, endAlpha, normalizedTime);
                color.a = newAlpha;

                _renderer.material.color = color;
                elapsedTime += Time.deltaTime;

                yield return null;
            }

            color.a = 0.0f;
            _renderer.material.color = color;

            Explode();
            Expire();
        }

        private void Explode()
        {
            float size = transform.localScale.magnitude;

            float explosionRadius = _baseExplosionRadius + _baseExplosionRadius / size;

            List<Rigidbody> nearbyRigidbodies = new List<Rigidbody>();

            Collider[] explodingColliders = Physics.OverlapSphere(transform.position, explosionRadius);

            foreach (Collider collider in explodingColliders)
            {
                if (collider.TryGetComponent(out Rigidbody rigidbody))
                {
                    nearbyRigidbodies.Add(rigidbody);
                }
            }

            ExplosionGenerator.Explode(nearbyRigidbodies, transform.position, explosionRadius, _explosionForce);
        }
    }
}