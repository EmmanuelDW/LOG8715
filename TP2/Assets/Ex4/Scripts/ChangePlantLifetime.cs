using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

[BurstCompile]
struct ChangePlantLifeTimeJob : IJobParallelFor
{
    public NativeArray<Vector3> preyTransformsPosition;
    public Vector3 transformPosition;
    public float touchingDistance;
    public float decreasingFactor;

    public void Execute(int index)
    {
        if (Vector3.Distance(preyTransformsPosition[index], transformPosition) < touchingDistance)
        {
            decreasingFactor *= 2f;
        } 
    }
}


public class ChangePlantLifetime : MonoBehaviour
{
    private Lifetime _lifetime;

    public void Start()
    {
        _lifetime = GetComponent<Lifetime>();
    }

    public void Update()
    {
        _lifetime.decreasingFactor = 1.0f;


        NativeArray<Vector3> preyTranformPositionArray = new NativeArray<Vector3>(Ex4Spawner.PreyTransforms.Length, Allocator.TempJob);
        for (int i = 0; i < Ex4Spawner.PreyTransforms.Length; i++)
        {
            preyTranformPositionArray[i] = Ex4Spawner.PreyTransforms[i].position;
        }

        ChangePlantLifeTimeJob changePlantLifeTimeJob = new ChangePlantLifeTimeJob
        {
            preyTransformsPosition = preyTranformPositionArray,
            transformPosition = transform.position,
            touchingDistance = Ex4Config.TouchingDistance,
            decreasingFactor = _lifetime.decreasingFactor
        };

        JobHandle jobHandle = changePlantLifeTimeJob.Schedule(Ex4Spawner.PreyTransforms.Length, 64);
        jobHandle.Complete();
        preyTranformPositionArray.Dispose();

    }


}