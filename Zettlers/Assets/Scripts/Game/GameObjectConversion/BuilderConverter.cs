using UnityEngine;
using Unity.Entities;
using System.Collections.Generic;
using Unity.Transforms;

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
            Vector2Int gameWorldPosition = position.Value.ToVector2Int();
            dstManager.AddComponentData(BuilderEntity,
                new GameWorldPosition { Position = gameWorldPosition });
        }

        public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
        {
            referencedPrefabs.Add(BuilderGameObject);
        }
    }
}