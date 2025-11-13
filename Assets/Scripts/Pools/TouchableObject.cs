using System;
using UnityEngine;

namespace Pools
{
    public abstract class TouchableObject : MonoBehaviour
    {
        public abstract void Touch();
        public abstract void UnTouch();
    }
}