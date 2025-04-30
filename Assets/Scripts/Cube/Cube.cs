using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    public bool HasCollidedWithPlatform { get; private set; }

    public void Touch()
    {
        if (HasCollidedWithPlatform == false)
        {
            HasCollidedWithPlatform = true;
        }
    }

    public void UnTouch()
    {
        if (HasCollidedWithPlatform)
        {
            HasCollidedWithPlatform = false;
        }
    }
}