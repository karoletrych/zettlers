using UnityEngine;
using Unity.Entities;
using System.Collections.Generic;
using Unity.Transforms;
using Unity.Mathematics;

namespace zettlers
{
    [DisallowMultipleComponent]
    [RequiresEntityConversion]
    public class BuilderConverter : MonoBehaviour, IDeclareReferencedPrefabs, IConvertGameObjectToEntity
    {
        public GameObject BuilderGameObject;
        public static Entity BuilderEntity;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            BuilderEntity = conversionSystem.GetPrimaryEntity(BuilderGameObject);

            Translation position = dstManager.GetComponentData<Translation>(entity);
            int2 gameWorldPosition = position.Value.ToInt2();
            dstManager.AddComponentData(BuilderEntity,
                new GameWorldPosition { Position = gameWorldPosition });
        }

        public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
        {
            referencedPrefabs.Add(BuilderGameObject);
        }
    }
}