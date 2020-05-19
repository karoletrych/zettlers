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
public class DisableCopySkinnedEntityDataToRenderEntitySystem : ComponentSystem
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

        Vector3 position;
        for (var x = 0; x < 5; x++)
        {
            for (var y = 0; y < 5; y++)
            {
                Entity instance = entityManager.Instantiate(PrefabEntities.ZettlerEntity);
                position = transform.TransformPoint(new float3(x * 1.3F, 6, y * 1.3F));
                entityManager.SetComponentData(instance, new Translation { Value = position });
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}