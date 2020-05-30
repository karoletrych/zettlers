using Unity.Entities;

namespace zettlers
{
    class BuildingBuiltSystem : LockstepSystem
    {
        protected override void OnLockstepUpdate()
        {
            // _jobQueue.Enqueue(new TakeProfessionJob
            // {
            //     Building = @event.Building
            // });
        }
    }
}
