using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    [SerializeField] private Color[] _colors;
    [SerializeField] private Color _defaultColor;
    
    private Renderer _renderer;

    public event Action<Cube> Expired;
    
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
    
    public void Touch()
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

    public void UnTouch()
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

        Expired?.Invoke(this);
    }
}