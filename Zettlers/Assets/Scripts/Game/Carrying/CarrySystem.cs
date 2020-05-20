using Unity.Entities;
using UnityEngine;

namespace zettlers
{
    class CarrySystem : SystemBase
    {
        protected override void OnUpdate()
        {
            EntityManager manager = World.DefaultGameObjectInjectionWorld.EntityManager;
            Entities
            .ForEach((
                Entity entity,
                ref Carrier carrier,
                ref GameWorldPosition pos) =>
            {
                if (carrier.Job != null)
                {
                    var dist = Vector2.Distance(pos.Position, carrier.Job.Value.SourcePosition.Value);
                    if (carrier.CarriedResource == null && dist < 2f)
                    {
                        manager.AddComponentData(entity,
                            new GoTowardsTarget
                            {
                                TargetPosition = carrier.Job.Value.TargetBuildingPosition
                            });
                        carrier.CarriedResource = carrier.Job.Value.ResourceType;
                    }
                    else if (carrier.CarriedResource == null)
                    {
                        manager.AddComponentData(entity,
                            new GoTowardsTarget
                            {
                                TargetPosition = carrier.Job.Value.SourcePosition.Value
                            });
                    }
                    else
                    {
                        manager.AddComponentData(entity,
                            new GoTowardsTarget
                            {
                                TargetPosition = carrier.Job.Value.TargetBuildingPosition
                            });
                    }
                }
            });
        }
    }
}