using UnityEngine;

namespace Classic
{
    public class SpawnManager : MonoBehaviour
    {
        public GameObject Prefab;
        public int Count = 1000;

        void Start()
        {
            for (int i = 0; i < Count; i++)
            {
                Vector3 pos = new Vector3(Random.Range(-50, 50), 0, Random.Range(-50, 50));
                Instantiate(Prefab, pos, Quaternion.identity);
            }
        }
    }
}