using Unity.Entities;

namespace zettlers
{
    class CarrySystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            EntityManager manager = World.DefaultGameObjectInjectionWorld.EntityManager;
            Entities
            .ForEach((
                Entity entity,
                ref Carrier carrier) =>
            {
                if (carrier.Job != null)
                {
                    manager.AddComponentData(entity,
                        new GoTowardsTarget
                        {
                            TargetPosition = carrier.Job.Value.TargetBuildingPosition
                        });
                }
            });
        }
    }
}