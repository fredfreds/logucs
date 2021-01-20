using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Jobs;

public class MoveSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var jobHandle = Entities.WithName("MoveSystem")
            .ForEach((ref Translation position, ref Rotation rotation) =>
            {
                //position.Value += 1f * math.forward(rotation.Value);
                position.Value.y += 1f;

                //if (position.Value.z > 50)
                if (position.Value.y > 50)
                {
                    //position.Value.z *= -1;
                    position.Value.y = 0;
                }
                //else if (position.Value.z < -50)
                {
                    //position.Value.z *= -1;
                }
            })
            .Schedule(inputDeps);

        return jobHandle;
    }
}
