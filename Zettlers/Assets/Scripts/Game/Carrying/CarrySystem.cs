using Unity.Entities;
using UnityEngine;

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
                ref Carrier carrier,
                ref GameWorldPosition pos) =>
            {
                if (carrier.Job != null)
                {
                    var dist = Vector2.Distance(pos.Position, carrier.Job.Value.SourcePosition.Value);
                    Debug.Log(dist);
                    if (!carrier.CarriesResource && dist < 2f)
                    {
                        Debug.Log("Touched resource");
                        manager.AddComponentData(entity,
                            new GoTowardsTarget
                            {
                                TargetPosition = carrier.Job.Value.TargetBuildingPosition
                            });
                        carrier.CarriesResource = true;
                    }
                    else if (!carrier.CarriesResource)
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