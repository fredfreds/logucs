using UnityEngine;
using UnityEngine.Jobs;
using Unity.Jobs;

namespace Classic
{
    public class SpawnParallelManager : MonoBehaviour
    {
        // 1
        struct MoveJob : IJobParallelForTransform
        {
            public void Execute(int index, TransformAccess transform)
            {
                transform.position += 10f * (transform.rotation * new Vector3(0, 0, 1));

                if (transform.position.z > 50)
                {
                    transform.position =
                        new Vector3(transform.position.x, 0, transform.position.z * -1);
                }
                else if (transform.position.z < -50)
                {
                    transform.position =
                        new Vector3(transform.position.x, 0, transform.position.z * -1);
                }

            }
        }

        // 2
        private MoveJob moveJob;
        private JobHandle jobHandle;
        private TransformAccessArray transforms;

        public GameObject Prefab;
        public float Speed = 0.1f;
        public int Count = 1000;

        private Transform[] prefabs;

        private void Start()
        {
            prefabs = new Transform[Count];

            for (int i = 0; i < Count; i++)
            {
                Vector3 pos = new Vector3(Random.Range(-50, 50), 0, Random.Range(-50, 50));
                GameObject pref = Instantiate(Prefab, pos, Quaternion.identity);
                prefabs[i] = pref.transform;
            }

            // 3
            transforms = new TransformAccessArray(prefabs);
        }

        private void Update()
        {
            // 4
            moveJob = new MoveJob();
            jobHandle = moveJob.Schedule(transforms);
        }

        // 5
        private void LateUpdate()
        {
            jobHandle.Complete();
        }

        // 6
        private void OnDestroy()
        {
            transforms.Dispose();
        }
    }
}