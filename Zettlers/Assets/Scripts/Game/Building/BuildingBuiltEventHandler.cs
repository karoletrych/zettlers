using Unity.Entities;

namespace zettlers
{
    class BuildingBuiltSystem : LockstepSystem
    {
        protected override void OnUpdate()
        {
            // _jobQueue.Enqueue(new TakeProfessionJob
            // {
            //     Building = @event.Building
            // });
        }
    }
}
