using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Classic
{
    public class Move : MonoBehaviour
    {
        public float Speed = 0.1f; 

        void Update()
        {
            transform.Translate(0, 0, Speed);

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
}