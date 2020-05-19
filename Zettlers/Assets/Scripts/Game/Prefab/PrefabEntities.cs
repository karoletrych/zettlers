using UnityEngine;
using Unity.Entities;
using System.Collections.Generic;

namespace zettlers
{
    public class PrefabEntities : MonoBehaviour, IDeclareReferencedPrefabs, IConvertGameObjectToEntity
    {
        public GameObject ZettlerGameObject;
        public static Entity ZettlerEntity;

        public GameObject BuildingSpaceGameObject;
        public static Entity BuildingSpaceEntity;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            ZettlerEntity = conversionSystem.GetPrimaryEntity(ZettlerGameObject);
            BuildingSpaceEntity = conversionSystem.GetPrimaryEntity(BuildingSpaceGameObject);
        }

        public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
        {
            referencedPrefabs.Add(ZettlerGameObject);
            referencedPrefabs.Add(BuildingSpaceGameObject);

        }
    }
}