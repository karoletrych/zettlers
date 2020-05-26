using UnityEngine;
using Unity.Entities;
using System.Collections.Generic;
using Unity.Transforms;
using Unity.Mathematics;

namespace zettlers
{
    [DisallowMultipleComponent]
    [RequiresEntityConversion]
    public class CarrierConverter : MonoBehaviour, IDeclareReferencedPrefabs, IConvertGameObjectToEntity
    {
        public GameObject ZettlerGameObject;
        public static Entity ZettlerEntity;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            ZettlerEntity = conversionSystem.GetPrimaryEntity(ZettlerGameObject);

            Translation position = dstManager.GetComponentData<Translation>(entity);
            int2 gameWorldPosition = position.Value.ToInt2();
            dstManager.AddComponentData(ZettlerEntity,
                new GameWorldPosition { Position = gameWorldPosition });
        }

        public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
        {
            referencedPrefabs.Add(ZettlerGameObject);
        }
    }
}