using Unity.Entities;

namespace zettlers
{
    class BuildingBuiltSystem : LockstepSystem
    {
        protected override void OnTick()
        {
            // _jobQueue.Enqueue(new TakeProfessionJob
            // {
            //     Building = @event.Building
            // });
        }
    }
}
