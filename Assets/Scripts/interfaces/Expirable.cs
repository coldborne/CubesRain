using System;
using UnityEngine;

namespace interfaces
{
    public interface IExpirable<T> where T : MonoBehaviour
    {
        public event Action<T> Expired;

        public void Expire();
    }
}