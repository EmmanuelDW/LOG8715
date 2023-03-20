using UnityEngine;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

[BurstCompile]
struct ChangePredatorLifetimeJob : IJobParallelFor
{
    public NativeArray<Vector3> predatorTransformsPosition;
    public Vector3 transformPosition;
    public float touchingDistance;
    public bool reproduced;

    public void Execute(int index)
    {
        if (Vector3.Distance(predatorTransformsPosition[index], transformPosition) < touchingDistance)
        {
            reproduced = true;
        }
    }
}

[BurstCompile]
struct ChangePredatorLifetimeJob2 : IJobParallelFor
{
    public NativeArray<Vector3> preyTransformsPosition;
    public Vector3 transformPosition;
    public float touchingDistance;
    public float decreasingFactor;

    public void Execute(int index)
    {
        if (Vector3.Distance(preyTransformsPosition[index], transformPosition) < touchingDistance)
        {
            decreasingFactor /= 2;
        }
    }
}


public class ChangePredatorLifetime : MonoBehaviour
{
    private Lifetime _lifetime;
    
    public void Start()
    {
        _lifetime = GetComponent<Lifetime>();
    }

    public void Update()
    {
        _lifetime.decreasingFactor = 1.0f;
        NativeArray<Vector3> predatorTransformPositionArray = new NativeArray<Vector3>(Ex4Spawner.PredatorTransforms.Length, Allocator.TempJob);
        NativeArray<Vector3> preyTransformPositionArray = new NativeArray<Vector3>(Ex4Spawner.PreyTransforms.Length, Allocator.TempJob);
        for (int i = 0; i < Ex4Spawner.PredatorTransforms.Length; i++)
        {
            predatorTransformPositionArray[i] = Ex4Spawner.PredatorTransforms[i].position;
        }
        for (int i = 0; i < Ex4Spawner.PreyTransforms.Length; i++)
        {
            preyTransformPositionArray[i] = Ex4Spawner.PreyTransforms[i].position;
        }

        ChangePredatorLifetimeJob changePredatorLifetimeJob = new ChangePredatorLifetimeJob
        {
            predatorTransformsPosition = predatorTransformPositionArray,
            transformPosition = transform.position,
            touchingDistance = Ex4Config.TouchingDistance,
            reproduced = _lifetime.reproduced
        };
        ChangePredatorLifetimeJob2 changePredatorLifetimeJob2 = new ChangePredatorLifetimeJob2
        {
            preyTransformsPosition = preyTransformPositionArray,
            transformPosition = transform.position,
            touchingDistance = Ex4Config.TouchingDistance,
            decreasingFactor = _lifetime.decreasingFactor
        };

        JobHandle jobHandlePredator = changePredatorLifetimeJob.Schedule(Ex4Spawner.PredatorTransforms.Length, 64);
        JobHandle jobHandlePrey = changePredatorLifetimeJob2.Schedule(Ex4Spawner.PreyTransforms.Length, 64);
        jobHandlePredator.Complete();
        jobHandlePrey.Complete();
        predatorTransformPositionArray.Dispose();
        preyTransformPositionArray.Dispose();
    }

}