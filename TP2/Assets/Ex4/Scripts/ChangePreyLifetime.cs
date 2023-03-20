using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public class ChangePreyLifetime : MonoBehaviour
{
    private Lifetime _lifetime;
    
    public void Start()
    {
        _lifetime = GetComponent<Lifetime>();
    }

    public void Update()
    {
        _lifetime.decreasingFactor = 1.0f;
        NativeArray<Vector3> plantTransformPositionArray = new NativeArray<Vector3>(Ex4Spawner.PlantTransforms.Length, Allocator.TempJob);
        NativeArray<Vector3> predatorTransformPositionArray = new NativeArray<Vector3>(Ex4Spawner.PredatorTransforms.Length, Allocator.TempJob);
        NativeArray<Vector3> preyTransformPositionArray = new NativeArray<Vector3>(Ex4Spawner.PreyTransforms.Length, Allocator.TempJob);
        for (int i = 0; i < Ex4Spawner.PlantTransforms.Length; i++)
        {
            plantTransformPositionArray[i] = Ex4Spawner.PlantTransforms[i].position;
        }
        for (int i = 0; i < Ex4Spawner.PredatorTransforms.Length; i++)
        {
            predatorTransformPositionArray[i] = Ex4Spawner.PredatorTransforms[i].position;
        }
        for (int i = 0; i < Ex4Spawner.PreyTransforms.Length; i++)
        {
            preyTransformPositionArray[i] = Ex4Spawner.PreyTransforms[i].position;
        }


        ChangePreyLifetimePlantTranformeJob changePreyLifetimePlantTranformeJob = new ChangePreyLifetimePlantTranformeJob
        {
            plantTransformsPosition = plantTransformPositionArray,
            transformPosition = transform.position,
            touchingDistance = Ex4Config.TouchingDistance,
            decreasingFactor = _lifetime.decreasingFactor
        };
        ChangePreyLifetimePredatorTranformeJob changePreyLifetimePredatorTranformeJob = new ChangePreyLifetimePredatorTranformeJob
        {
            predatorTransformsPosition = predatorTransformPositionArray,
            transformPosition = transform.position,
            touchingDistance = Ex4Config.TouchingDistance,
            decreasingFactor = _lifetime.decreasingFactor
        };
        ChangePreyLifetimePreyTranformeJob changePreyLifetimePreyTranformeJob = new ChangePreyLifetimePreyTranformeJob
        {
            preyTransformsPosition = preyTransformPositionArray,
            transformPosition = transform.position,
            touchingDistance = Ex4Config.TouchingDistance,
            reproduced = _lifetime.reproduced
        };

        JobHandle jobHandlePlant = changePreyLifetimePlantTranformeJob.Schedule(Ex4Spawner.PlantTransforms.Length, 64);
        JobHandle jobHandlePredator = changePreyLifetimePredatorTranformeJob.Schedule(Ex4Spawner.PredatorTransforms.Length, 64);
        JobHandle jobHandlePrey = changePreyLifetimePreyTranformeJob.Schedule(Ex4Spawner.PreyTransforms.Length, 64);
        jobHandlePlant.Complete();
        jobHandlePredator.Complete();
        jobHandlePrey.Complete();
        plantTransformPositionArray.Dispose();
        predatorTransformPositionArray.Dispose();
        preyTransformPositionArray.Dispose();
    }
}

[BurstCompile]
struct ChangePreyLifetimePlantTranformeJob : IJobParallelFor
{
    public NativeArray<Vector3> plantTransformsPosition;
    public Vector3 transformPosition;
    public float touchingDistance;
    public float decreasingFactor;

    public void Execute(int index)
    {
        if (Vector3.Distance(plantTransformsPosition[index], transformPosition) < touchingDistance)
        {
            decreasingFactor /= 2;
        }
    }
}

[BurstCompile]
struct ChangePreyLifetimePredatorTranformeJob : IJobParallelFor
{
    public NativeArray<Vector3> predatorTransformsPosition;
    public Vector3 transformPosition;
    public float touchingDistance;
    public float decreasingFactor;

    public void Execute(int index)
    {
        if (Vector3.Distance(predatorTransformsPosition[index], transformPosition) < touchingDistance)
        {
            decreasingFactor *= 2f;
        }
    }
}

[BurstCompile]
struct ChangePreyLifetimePreyTranformeJob : IJobParallelFor
{
    public NativeArray<Vector3> preyTransformsPosition;
    public Vector3 transformPosition;
    public float touchingDistance;
    public bool reproduced;

    public void Execute(int index)
    {
        if (Vector3.Distance(preyTransformsPosition[index], transformPosition) < touchingDistance)
        {
            reproduced = true;
        }
    }
}