using UnityEngine;
using Unity.Entities;
using System.Collections.Generic;

namespace zettlers
{
    public class ZettlerConverter : MonoBehaviour, IDeclareReferencedPrefabs, IConvertGameObjectToEntity
    {
        public GameObject ZettlerGameObject;
        public static Entity ZettlerEntity;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            ZettlerEntity = conversionSystem.GetPrimaryEntity(ZettlerGameObject);
        }

        public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
        {
            referencedPrefabs.Add(ZettlerGameObject);
        }
    }
}