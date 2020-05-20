using UnityEngine;
using Unity.Entities;
using System.Collections.Generic;

namespace zettlers
{
    public class WoodConverter : MonoBehaviour, IConvertGameObjectToEntity
    {
        public static Entity WoodEntity;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponent(entity, typeof(Wood));
            WoodEntity = entity;
        }

    }
}