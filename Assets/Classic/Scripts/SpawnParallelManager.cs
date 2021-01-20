using UnityEngine;

namespace Classic
{
    public class SpawnParallelManager : MonoBehaviour
    {
        public GameObject Prefab;
        public float Speed = 0.1f;
        public int Count = 1000;

        private GameObject[] prefabs;

        void Start()
        {
            prefabs = new GameObject[Count];

            for (int i = 0; i < Count; i++)
            {
                Vector3 pos = new Vector3(Random.Range(-50, 50), 0, Random.Range(-50, 50));
                prefabs[i] = Instantiate(Prefab, pos, Quaternion.identity);
            }
        }

        void Update()
        {
            for (int i = 0; i < prefabs.Length; i++)
            {
                prefabs[i].transform.Translate(0, 0, Speed);

                if (prefabs[i].transform.position.z > 50)
                {
                    prefabs[i].transform.position =
                        new Vector3(prefabs[i].transform.position.x, 0, prefabs[i].transform.position.z * -1);
                }
                else if (transform.position.z < -50)
                {
                    prefabs[i].transform.position =
                        new Vector3(prefabs[i].transform.position.x, 0, prefabs[i].transform.position.z * -1);
                }
            }
        }
    }
}