using System;
using UnityEngine;

namespace Pools
{
    public abstract class TouchableObject<T> : MonoBehaviour where T : MonoBehaviour
    {
        public event Action<T> Expired;

        public abstract void Touch();
        public abstract void UnTouch();

        public void Expire()
        {
            Expired?.Invoke(this as T);
        }
    }
}