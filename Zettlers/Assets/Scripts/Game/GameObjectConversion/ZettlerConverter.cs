using UnityEngine;
using Unity.Entities;
using System.Collections.Generic;
using Unity.Transforms;

namespace zettlers
{
    public class ZettlerConverter : MonoBehaviour, IDeclareReferencedPrefabs, IConvertGameObjectToEntity
    {
        public GameObject ZettlerGameObject;
        public static Entity ZettlerEntity;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            ZettlerEntity = conversionSystem.GetPrimaryEntity(ZettlerGameObject);

            Translation position = dstManager.GetComponentData<Translation>(entity);
            Vector2Int gameWorldPosition = position.Value.ToVector2Int();
            dstManager.AddComponentData(ZettlerEntity, 
                new GameWorldPosition{Position = gameWorldPosition});
        }

        public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
        {
            referencedPrefabs.Add(ZettlerGameObject);
        }
    }
}