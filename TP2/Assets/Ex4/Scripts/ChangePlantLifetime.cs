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
    //public Lifetime lifetime;
    public float decreasingFactor;

    public void Execute(int index)
    {
        if (Vector3.Distance(preyTransformsPosition[index], transformPosition) < touchingDistance)
        {
            // lifetime.decreasingFactor *= 2f;
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
        // _lifetime.decreasingFactor = 1.0f;
        //foreach (var prey in Ex4Spawner.PreyTransforms)
        //{
        //    if (Vector3.Distance(prey.position, transform.position) < Ex4Config.TouchingDistance)
        //    {
        //        _lifetime.decreasingFactor *= 2f;
        //        break;
        //    }

        //}
        _lifetime.decreasingFactor = 1.0f;
        Vector3[] preyPositions = new Vector3[Ex4Spawner.PreyTransforms.Length];
        for (int i = 0; i < Ex4Spawner.PreyTransforms.Length; i++)
        {
            preyPositions[i] = Ex4Spawner.PreyTransforms[i].position;
        }
        ExecuteChangePlantLifeTimeJob(preyPositions);
    }

    public void ExecuteChangePlantLifeTimeJob(Vector3[] preyTransformPosition) 
    {

        var preyTransformPositionArray = new NativeArray<Vector3>(preyTransformPosition, Allocator.TempJob);
        var job = new ChangePlantLifeTimeJob
        {
            preyTransformsPosition = preyTransformPositionArray,
            transformPosition = transform.position,
            touchingDistance = Ex4Config.TouchingDistance,
            // lifetime = _lifetime,
            decreasingFactor = _lifetime.decreasingFactor


        };
        var jobHandle = job.Schedule(preyTransformPositionArray.Length, 64);
        jobHandle.Complete();
        preyTransformPositionArray.CopyTo(preyTransformPosition);
        preyTransformPositionArray.Dispose();

    }

}