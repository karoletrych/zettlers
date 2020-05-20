using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;

namespace zettlers
{
    class GoTowardsTargetSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            Entities
            .ForEach((
                ref GameWorldPosition carrierWorldPosition, 
                ref Translation translation, 
                ref GoTowardsTarget @goto,
                ref Rotation rotation) =>
            {

                Vector3 heading = (@goto.TargetPosition - carrierWorldPosition.Position).ToVector3();

                heading.y = 0f;
                rotation.Value = quaternion.LookRotation(heading, math.up());

                translation.Value += (Time.DeltaTime * math.forward(rotation.Value));
                carrierWorldPosition.Position = translation.Value.ToVector2Int();
            });
        }
    }
}