using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

public class MovePredatorTowardPrey : MonoBehaviour
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

        NativeArray<Vector3> preyTransformsPositionArray = new NativeArray<Vector3>(Ex4Spawner.PreyTransforms.Length, Allocator.TempJob);
        for (int i = 0; i < Ex4Spawner.PreyTransforms.Length; i++)
        {
            preyTransformsPositionArray[i] = Ex4Spawner.PreyTransforms[i].position;
        }

        MovePredatorTowardPreyJob movePreyTowardPlantJob = new MovePredatorTowardPreyJob
        {
            preyPosition = preyTransformsPositionArray,
            transformPosition = transform.position,
            closestDistance = closestDistance,
            closestPosition = closestPosition
        };

        JobHandle jobHandle = movePreyTowardPlantJob.Schedule(Ex4Spawner.PlantTransforms.Length, 64);
        jobHandle.Complete();
        preyTransformsPositionArray.Dispose();
        _velocity.velocity = (closestPosition - transform.position) * Ex4Config.PreySpeed;


    }
}

struct MovePredatorTowardPreyJob : IJobParallelFor
{
    public NativeArray<Vector3> preyPosition;
    public Vector3 transformPosition;
    public float closestDistance;
    public Vector3 closestPosition;

    public void Execute(int index)
    {
        var distance = Vector3.Distance(preyPosition[index], transformPosition);
        if (distance < closestDistance)
        {
            closestDistance = distance;
            closestPosition = preyPosition[index];
        }
    }
}
