using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace zettlers
{
    class BuilderJobAssignerSystem : SystemBase
    {
        private EntityQuery _buildersQuery;

        protected override void OnCreate()
        {
            _buildersQuery = GetEntityQuery(typeof(Builder));
        }
        protected override void OnUpdate()
        {
            NativeArray<Entity> builders =
                _buildersQuery.ToEntityArray(Allocator.Temp);

            NativeList<Entity> freebuilders = new NativeList<Entity>(Allocator.Temp);
            for (int i = 0; i < builders.Length; i++)
            {
                Builder builderData = GetComponentDataFromEntity<Builder>(true)[builders[i]];
                if (builderData.Job == null)
                {
                    freebuilders.Add(builders[i]);
                }
            }

            while (BuilderJobQueue.Queue.Count != 0)
            {
                BuildJob job = BuilderJobQueue.Queue.Peek();

                if (freebuilders.Length == 0)
                {
                    freebuilders.Dispose();
                    return;
                }

                int minDistbuilderIdx = -1;
                Entity minDistbuilder = freebuilders[0];
                float minDistbuilderToJob = float.MaxValue;
                for (int i = 0; i < freebuilders.Length; i++)
                {
                    Entity builder = freebuilders[i];
                    GameWorldPosition builderPositionData =
                        GetComponentDataFromEntity<GameWorldPosition>(true)[builder];

                    float builderToJobDistance = Vector2.Distance(
                        builderPositionData.Position,
                        job.Building.Position
                        );

                    if (builderToJobDistance < minDistbuilderToJob)
                    {
                        minDistbuilderIdx = i;
                        minDistbuilderToJob = builderToJobDistance;
                        minDistbuilder = builder;
                    }
                }
                EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

                BuilderJobQueue.Queue.Dequeue();
                entityManager.SetComponentData(minDistbuilder, new Builder { Job = job });

                freebuilders.RemoveAtSwapBack(minDistbuilderIdx);
            }

            freebuilders.Dispose();
        }
    }
}