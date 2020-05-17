using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;
using Unity.Rendering;

public class LevelUpSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref LevelComponent levelComponent) =>
        {
            levelComponent.level += 1f * Time.DeltaTime;
        });
    }
}

public class MoverSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref Translation translation, ref MoveSpeedComponent msc) =>
        {
            translation.Value.y += msc.moveSpeed * Time.DeltaTime;
            if (translation.Value.y > 5f)
            {
                msc.moveSpeed = -Mathf.Abs(msc.moveSpeed);
            }
            if (translation.Value.y < -5f)
            {
                msc.moveSpeed = +Mathf.Abs(msc.moveSpeed);
            }
        });
    }
}

public struct LevelComponent : IComponentData
{
    public float level;
}
public struct MoveSpeedComponent : IComponentData
{
    public float moveSpeed;
}

public class Testing : MonoBehaviour
{
[SerializeField] private Mesh _mesh;
[SerializeField] private Material _material;

    // Start is called before the first frame update
    void Start()
    {
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        EntityArchetype entityArchetype = entityManager.CreateArchetype(
                typeof(LevelComponent),
                typeof(Translation),
                typeof(RenderMesh),
                typeof(LocalToWorld),
                typeof(MoveSpeedComponent)
        );

        NativeArray<Entity> entityArray = new NativeArray<Entity>(10000, Allocator.Temp);
        entityManager.CreateEntity(entityArchetype, entityArray);

        for (int i = 0; i < entityArray.Length; i++)
        {
            Entity entity = entityArray[i];
            entityManager.SetComponentData(entity, new LevelComponent{ level = Random.Range(10, 20)});
            entityManager.SetComponentData(entity, new Translation { Value = new Unity.Mathematics.float3(Random.Range(-8, 8f), Random.Range(-5, 5f), 0) });
            entityManager.SetComponentData(entity, new MoveSpeedComponent { moveSpeed = UnityEngine.Random.Range(1f, 2f) });
            entityManager.SetSharedComponentData(entity, new RenderMesh {
                material = _material,
                mesh = _mesh
            });
        }

        entityArray.Dispose();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
