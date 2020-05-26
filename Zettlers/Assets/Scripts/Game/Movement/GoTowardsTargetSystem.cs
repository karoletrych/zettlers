using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;

namespace zettlers
{
    class GoTowardsTargetSystem : LockstepSystem
    {
        private const int Velocity = 2;

        protected override void OnTick()
        {
            Entities
            .ForEach((
                ref GameWorldPosition carrierWorldPosition, 
                ref Translation translation, 
                ref GoTowardsTarget @goto,
                ref Rotation rotation) =>
            {

                float3 heading = (@goto.TargetPosition - carrierWorldPosition.Position).ToFloat3();

                heading.y = 0f;
                rotation.Value = quaternion.LookRotation(heading, math.up());

                translation.Value += (Time.DeltaTime * Velocity * math.forward(rotation.Value));
                carrierWorldPosition.Position = translation.Value.ToInt2();
            })
            .WithoutBurst()
            .Run();
        }
    }
}