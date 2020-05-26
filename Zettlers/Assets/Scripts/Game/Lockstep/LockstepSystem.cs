using Unity.Entities;

namespace zettlers
{
    class BaseSystem : LockstepSystem
    {
        override OnUpdate()
        {
            if (TickManager.ReadyToMoveToNextTick())
                OnTick();
        }

        protected virtual OnTick()
        {
        }
    }    
}
