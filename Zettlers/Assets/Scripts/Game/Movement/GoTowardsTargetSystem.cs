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
                ref GameWorldPosition gameWorldPosition, 
                ref Translation translation, 
                ref GoTowardsTarget @goto,
                ref Rotation rotation) =>
            {
                Vector2 heading = @goto.TargetPosition - gameWorldPosition.Position;
                heading.y = 0f;
                // rotation.Value = quaternion.LookRotation(heading, math.up());

                translation.Value += (Time.DeltaTime * math.forward(rotation.Value));
            });
        }
    }
}