using Unity.Entities;

namespace zettlers
{
    public class LockstepSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            if (true)
                OnTick();
        }

        protected virtual void OnTick()
        {
        }
    }    
}
