using Unity.Entities;

namespace zettlers
{
    class BuildingBuildSystem : LockstepSystem
    {
        protected override void OnUpdate()
        {
            // _builderJobQueue.Enqueue(new BuildJob
            // {
            //     ResourceType = @event.ResourceType,
            //     Building = @event.Building
            // });
        }
    }
}
