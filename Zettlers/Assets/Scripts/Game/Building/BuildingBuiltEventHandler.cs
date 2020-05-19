using Unity.Entities;

namespace zettlers
{
    class BuildingBuiltSystem : ComponentSystem
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
