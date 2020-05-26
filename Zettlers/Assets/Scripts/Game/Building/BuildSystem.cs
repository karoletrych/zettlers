using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace zettlers
{
    public class BuildSystem : LockstepSystem
    {
        protected override void OnTick()
        {
            EntityCommandBuffer entityCommandBuffer =
                World
                .GetExistingSystem<EndSimulationEntityCommandBufferSystem>()
                .CreateCommandBuffer();

            Entities
            .ForEach((
                Entity entity,
                ref Builder builder,
                ref GameWorldPosition pos) =>
            {
                if (builder.Job != null)
                {
                    var distToTarget = Vector2.Distance(pos.Position, builder.Job.Value.Building.Position);

                    if (distToTarget < 2f && builder.CurrentBuildTime < 4f)
                    {
                        entityCommandBuffer.RemoveComponent(entity, typeof(GoTowardsTarget));

                        builder.CurrentBuildTime += Time.DeltaTime;
                    }
                    else if (distToTarget < 2f && builder.CurrentBuildTime >= 4f)
                    {
                        entityCommandBuffer.RemoveComponent(entity, typeof(GoTowardsTarget));

                        ComponentDataFromEntity<ConstructionSite> constructionSiteDataFromEntity = GetComponentDataFromEntity<ConstructionSite>();
                        ConstructionSite constructionSiteData = constructionSiteDataFromEntity[builder.Job.Value.Building.Entity];
                        int woodStillRequired = constructionSiteData.WoodStillRequired;
                        if (woodStillRequired == 1) //last resource nailed
                        {

                            entityCommandBuffer.DestroyEntity(builder.Job.Value.Building.Entity);

                            Entity newBuilding = entityCommandBuffer.Instantiate(BuildingConverter.BuildingEntity);

                            Vector2Int newBuildingPosition = builder.Job.Value.Building.Position;
                            entityCommandBuffer.AddComponent(newBuilding, typeof(GameWorldPosition));
                            entityCommandBuffer.SetComponent(newBuilding, new GameWorldPosition{Position = newBuildingPosition});
                            entityCommandBuffer.SetComponent(newBuilding, new Translation {Value = newBuildingPosition.ToVector3()});

                            entityCommandBuffer.RemoveComponent(builder.Job.Value.Building.Entity, typeof(ConstructionSite));
                        }
                        else
                        {
                            constructionSiteDataFromEntity[builder.Job.Value.Building.Entity] = new ConstructionSite { 
                                WoodStillRequired = woodStillRequired - 1 
                            };
                        }

                        builder.Job = null;
                        builder.CurrentBuildTime = 0;
                    }
                    else
                    {
                        entityCommandBuffer.AddComponent(entity, new GoTowardsTarget
                        {
                            TargetPosition = builder.Job.Value.Building.Position
                        });
                    }
                }
            })
            .WithoutBurst()
            .Run();
        }
    }
}
