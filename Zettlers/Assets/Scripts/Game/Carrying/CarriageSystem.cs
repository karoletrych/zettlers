using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace zettlers
{
    class CarriageSystem : LockstepSystem
    {

        public NativeQueue<BuildJob> BuilderJobQueue;

        protected override void OnCreate()
        {
            BuilderJobQueue = new NativeQueue<BuildJob>(Allocator.Persistent);
        }

        protected override void OnDestroy()
        {
            BuilderJobQueue.Dispose();
        }

        protected override void OnLockstepUpdate()
        {
            EntityCommandBuffer entityCommandBuffer =
                World
                .GetExistingSystem<EndSimulationEntityCommandBufferSystem>()
                .CreateCommandBuffer();

            var builderJobQueue = BuilderJobQueue;
            Entities
            .ForEach((
                Entity entity,
                ref Carrier carrier,
                ref GameWorldPosition pos) =>
            {
                if (carrier.Job != null)
                {
                    var distToSource = math.distance(pos.Position, carrier.Job.Value.SourcePosition.Value);
                    var distToTarget = math.distance(pos.Position, carrier.Job.Value.TargetBuilding.Position);

                    if (carrier.CarriedResource == null && distToSource < 2f)
                    {
                        entityCommandBuffer.AddComponent(entity, new GoTowardsTarget
                        {
                            TargetPosition = carrier.Job.Value.TargetBuilding.Position
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
                        entityCommandBuffer.RemoveComponent<GoTowardsTarget>(entity);

                        builderJobQueue.Enqueue(new BuildJob {
                            Building = carrier.Job.Value.TargetBuilding,
                            ResourceType = carrier.CarriedResource.Value
                        });

                        carrier.CarriedResource = null;
                        carrier.Job = null;
                    }
                    else
                    {
                        entityCommandBuffer.AddComponent(entity,
                            new GoTowardsTarget
                            {
                                TargetPosition = carrier.Job.Value.TargetBuilding.Position
                            });
                    }
                }
            })
            .Run();
        }
    }
}