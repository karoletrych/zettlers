using Unity.Entities;

namespace zettlers
{
    class BuildingBuildSystem : ComponentSystem
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
