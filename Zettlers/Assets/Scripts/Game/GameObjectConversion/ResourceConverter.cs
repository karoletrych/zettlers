using UnityEngine;
using Unity.Entities;
using System.Collections.Generic;

namespace zettlers
{
    public class ResourceConverter : MonoBehaviour, IConvertGameObjectToEntity
    {
        public static Entity ResourceEntity;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponent(entity, typeof(Resource));
            dstManager.AddComponentData(entity, new GameWorldPosition{Position = new Vector2Int()});
            ResourceEntity = entity;
        }

    }
}