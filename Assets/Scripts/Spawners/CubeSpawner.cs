using Cubes;
using UnityEngine;

namespace Spawners
{
    public class CubeSpawner : Spawner<Cube>
    {
        [SerializeField] private BombSpawner _bombSpawner;

        protected override void Release(Cube item)
        {
            _bombSpawner.Spawn(item.transform.position);
            
            base.Release(item);
        }
    }
}