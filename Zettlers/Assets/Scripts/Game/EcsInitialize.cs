using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Rendering;
using Unity.Mathematics;
using zettlers;

/// <summary>
/// Suppresses the error: "ArgumentException: A component with type:BoneIndexOffset has not been added to the entity.", until the Unity bug is fixed.
/// </summary>
[UpdateInGroup(typeof(InitializationSystemGroup))]
public class DisableCopySkinnedEntityDataToRenderEntitySystem : SystemBase
{
    protected override void OnCreate()
    {
        World.GetOrCreateSystem<CopySkinnedEntityDataToRenderEntity>().Enabled = false;
    }

    protected override void OnUpdate() { }
}
public class EcsInitialize : MonoBehaviour
{
    void Start()
    {
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        for (var x = 0; x < 3; x++)
        {
            for (var y = 0; y < 3; y++)
            {
                Entity instance = entityManager.Instantiate(CarrierConverter.ZettlerEntity);
                Vector3 position = transform.TransformPoint(new float3(x * 1.3F, 6, y * 1.3F));
                entityManager.AddComponentData(instance, new Carrier { Job = null });
                entityManager.SetComponentData(instance, new Translation { Value = position });
            }
        }

        for (var y = 0; y < 3; y++)
        {
            Entity instance = entityManager.Instantiate(BuilderConverter.BuilderEntity);
            Vector3 position = transform.TransformPoint(new float3(5 + y * 1.3F, 6, y * 1.3F));
            entityManager.AddComponentData(instance, new Builder { Job = null });
            entityManager.SetComponentData(instance, new Translation { Value = position });
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}