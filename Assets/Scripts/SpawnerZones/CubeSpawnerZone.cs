using SpawnerZones;
using UnityEngine;

namespace Cubes
{
    [RequireComponent(typeof(BoxCollider))]
    public class CubeSpawnerZone : SpawnerZone<Cube>
    {
        private BoxCollider _boxCollider;

        protected override void Init()
        {
            _boxCollider = GetComponent<BoxCollider>();
        }

        protected override Vector3 GetPosition()
        {
            Bounds zoneBounds = _boxCollider.bounds;

            float minX = zoneBounds.min.x;
            float maxX = zoneBounds.max.x;

            float minZ = zoneBounds.min.z;
            float maxZ = zoneBounds.max.z;

            float x = Random.Range(minX, maxX);
            float z = Random.Range(minZ, maxZ);

            Vector3 position = new Vector3(x, zoneBounds.min.y, z);

            return position;
        }
    }
}