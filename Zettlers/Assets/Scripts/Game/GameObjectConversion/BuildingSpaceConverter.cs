using UnityEngine;
using Unity.Entities;
using System.Collections.Generic;

namespace zettlers
{
    [DisallowMultipleComponent]
    [RequiresEntityConversion]
    public class BuildingSpaceConverter : MonoBehaviour, IDeclareReferencedPrefabs, IConvertGameObjectToEntity
    {
        public GameObject BuildingSpaceGameObject;
        public static Entity BuildingSpaceEntity;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            BuildingSpaceEntity = conversionSystem.GetPrimaryEntity(BuildingSpaceGameObject);
        }

        public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
        {
            referencedPrefabs.Add(BuildingSpaceGameObject);
        }
    }
}