using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

public class MovePreyTowardPlant : MonoBehaviour
{
    private Velocity _velocity;
    
    public void Start()
    {
        _velocity = GetComponent<Velocity>();

    }

    public void Update()
    {
        var closestDistance = float.MaxValue;
        var closestPosition = transform.position;

        NativeArray<Vector3> plantTransformsPositionArray = new NativeArray<Vector3>(Ex4Spawner.PlantTransforms.Length, Allocator.TempJob);
        for (int i = 0; i < Ex4Spawner.PlantTransforms.Length; i++)
        {
            plantTransformsPositionArray[i] = Ex4Spawner.PlantTransforms[i].position;
        }

        MovePreyTowardPlantJob movePreyTowardPlantJob = new MovePreyTowardPlantJob
        {
            plantTransformsPosition = plantTransformsPositionArray,
            transformPosition = transform.position,
            closestDistance = closestDistance,
            closestPosition = closestPosition
        };

        JobHandle jobHandle = movePreyTowardPlantJob.Schedule(Ex4Spawner.PlantTransforms.Length, 64);
        jobHandle.Complete();
        plantTransformsPositionArray.Dispose();

        _velocity.velocity = (closestPosition - transform.position) * Ex4Config.PreySpeed;

    }
}

[BurstCompile]
struct MovePreyTowardPlantJob : IJobParallelFor
{
    public NativeArray<Vector3> plantTransformsPosition;
    public Vector3 transformPosition;
    public float closestDistance;
    public Vector3 closestPosition;
    public void Execute(int index)
    {
        var distance = Vector3.Distance(plantTransformsPosition[index], transformPosition);
        if (distance < closestDistance)
        {
            closestDistance = distance;
            closestPosition = plantTransformsPosition[index];
        }
    }
}