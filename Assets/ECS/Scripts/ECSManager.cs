using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace ECS
{
    public class ECSManager : MonoBehaviour
    {
        public GameObject Prefab;
        public int Count;

        private EntityManager manager;

        void Start()
        {
            manager = World.DefaultGameObjectInjectionWorld.EntityManager;

            var settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, null);
            var prefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(Prefab, settings);

            for (int i = 0; i < Count; i++)
            {
                var instance = manager.Instantiate(prefab);
                var position = transform.TransformPoint(new float3(UnityEngine.Random.Range(-50, 50), 0, UnityEngine.Random.Range(-50, 50)));

                manager.SetComponentData(instance, new Translation { Value = position });
                manager.SetComponentData(instance, new Rotation { Value = new quaternion(0, 0, 0, 0) });
            }
        }
    }
}