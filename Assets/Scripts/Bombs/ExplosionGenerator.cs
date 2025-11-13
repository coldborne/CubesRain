using System.Collections.Generic;
using UnityEngine;

namespace Bombs
{
    public static class ExplosionGenerator
    {
        public static void Explode(IEnumerable<Rigidbody> items, Vector3 position, float radius, float force)
        {
            foreach (Rigidbody item in items)
            {
                item.AddExplosionForce(force, position, radius);
            }
        }
    }
}