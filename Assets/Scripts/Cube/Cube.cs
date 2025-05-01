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

    public void Touch()
    {
        if (HasCollidedWithPlatform == false)
        {
            _renderer.material.color = _colors[Random.Range(0, _colors.Length)];
            HasCollidedWithPlatform = true;
            
            int minLifeTime = 2;
            int maxLifeTime = 5;
            
            int cubeLifeTime = Random.Range(minLifeTime, maxLifeTime);
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

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _renderer.material.color = _defaultColor;
    }
    
    private IEnumerator ExpireAfterDelay(int delay)
    {
        yield return new WaitForSeconds(delay);

        Expired?.Invoke(this);
    }
}