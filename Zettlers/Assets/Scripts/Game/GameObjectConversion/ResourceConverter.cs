using UnityEngine;
using Unity.Entities;
using System.Collections.Generic;
using Unity.Transforms;
using Unity.Mathematics;

namespace zettlers
{
    [DisallowMultipleComponent]
    [RequiresEntityConversion]
    public class ResourceConverter : MonoBehaviour, IConvertGameObjectToEntity
    {
        public static Entity ResourceEntity;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponent(entity, typeof(Resource));

            Translation position = dstManager.GetComponentData<Translation>(entity);
            int2 gameWorldPosition = position.Value.ToInt2();

            dstManager.AddComponentData(entity, new GameWorldPosition{Position = gameWorldPosition});
            ResourceEntity = entity;
        }
    }
}