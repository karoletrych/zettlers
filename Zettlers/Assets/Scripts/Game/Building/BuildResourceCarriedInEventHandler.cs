using Unity.Entities;

namespace zettlers
{
    class BuildingBuildSystem : LockstepSystem
    {
        protected override void OnLockstepUpdate()
        {
            // _builderJobQueue.Enqueue(new BuildJob
            // {
            //     ResourceType = @event.ResourceType,
            //     Building = @event.Building
            // });
        }
    }
}
