// using UnityEngine;
// using System.Collections.Generic;
// using System.Linq;
// using Unity.Entities;
// using Unity.Collections;

// namespace zettlers
// {
//     class BuilderJobAssignerSystem : SystemBase
//     {
//         private EntityQuery _buildersQuery;

//         protected override void OnCreate()
//         {
//             _buildersQuery = GetEntityQuery(typeof(Builder));
//         }

//         protected override void OnUpdate()
//         {
//             NativeArray<Builder> builders = 
//                 _buildersQuery.ToComponentDataArray<Builder>(Allocator.Temp);
//             List<Builder> freeBuilders = builders
//                 .Where(b => b.Job == null).ToList();

//             float minDist = float.MaxValue;
//             foreach (BuildJob job in _jobQueue.Queue)
//             {
//                 if (freeBuilders.Count == 0)
//                     return;

//                 Builder minDistBuilder = freeBuilders[0];
//                 foreach (Builder builder in freeBuilders)
//                 {
//                     float dist = Vector2.Distance(builder.Position, job.Building.Position);
//                     if (dist < minDist)
//                     {
//                         minDist = dist;
//                         minDistBuilder = builder;
//                     }
//                 }

//                 minDistBuilder.Job = job;
//                 freeBuilders.Remove(minDistBuilder);
//             }
//         }
//     }
// }
