using Cubes;

namespace Pools
{
    public class CubePool : ItemPool<Cube>
    {
        public override void Release(Cube item)
        {
            item.UnTouch();

            base.Release(item);
        }
    }
}