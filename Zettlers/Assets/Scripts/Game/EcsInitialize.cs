using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;
using Unity.Rendering;
using Unity.Mathematics;
using Random = UnityEngine.Random;

public class EcsInitialize : MonoBehaviour
{
    [SerializeField] private GameObject ZettlerPrefab;
    [SerializeField] private GameObject BuildingSpacePrefab;

    void Start()
    {
        GameObjectConversionSettings settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, null);
        Entity zettlerEntity = GameObjectConversionUtility.ConvertGameObjectHierarchy(ZettlerPrefab, settings);
        Entity buildingSpaceEntity = GameObjectConversionUtility.ConvertGameObjectHierarchy(BuildingSpacePrefab, settings);

        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        Vector3 position;
        for (var x = 0; x < 5; x++)
        { 
            for (var y = 0; y < 5; y++)
            {
                Entity instance = entityManager.Instantiate(zettlerEntity);
                position = transform.TransformPoint(new float3(x * 1.3F, 6, y * 1.3F));
                entityManager.SetComponentData(instance, new Translation {Value = position});
            }
        }
        Entity instancee = entityManager.Instantiate(buildingSpaceEntity);
        position = transform.TransformPoint(new float3(1 * 1.3F, 7, 1 * 1.3F));
        entityManager.SetComponentData(instancee, new Translation {Value = position});
    }

    // Update is called once per frame
    void Update()
    {

    }
}
