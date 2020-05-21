using Unity.Entities;
using UnityEngine;

namespace zettlers
{
    class CarrySystem : SystemBase
    {
        protected override void OnUpdate()
        {
            EntityCommandBuffer entityCommandBuffer =
                World
                .GetExistingSystem<EndSimulationEntityCommandBufferSystem>()
                .CreateCommandBuffer();

            Entities
            .ForEach((
                Entity entity,
                ref Carrier carrier,
                ref GameWorldPosition pos) =>
            {
                if (carrier.Job != null)
                {
                    var distToSource = Vector2.Distance(pos.Position, carrier.Job.Value.SourcePosition.Value);
                    var distToTarget = Vector2.Distance(pos.Position, carrier.Job.Value.TargetBuildingPosition);

                    if (carrier.CarriedResource == null && distToSource < 2f)
                    {
                        entityCommandBuffer.AddComponent(entity, new GoTowardsTarget
                        {
                            TargetPosition = carrier.Job.Value.TargetBuildingPosition
                        });
                        entityCommandBuffer.DestroyEntity(carrier.Job.Value.ResourceToCarry);

                        carrier.CarriedResource = carrier.Job.Value.ResourceType;
                    }
                    else if (carrier.CarriedResource == null)
                    {
                        entityCommandBuffer.AddComponent(entity, new GoTowardsTarget
                        {
                            TargetPosition = carrier.Job.Value.SourcePosition.Value
                        });
                    }
                    else if (carrier.CarriedResource != null && distToTarget < 2f)
                    {
                        carrier.Job = null;
                        entityCommandBuffer.RemoveComponent(entity, typeof(GoTowardsTarget));
                    }
                    else
                    {
                        entityCommandBuffer.AddComponent(entity,
                            new GoTowardsTarget
                            {
                                TargetPosition = carrier.Job.Value.TargetBuildingPosition
                            });
                    }
                }
            })
            .Run();
        }
    }
}