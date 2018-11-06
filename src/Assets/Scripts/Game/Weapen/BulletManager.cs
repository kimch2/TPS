using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Weapen
{
    public class BulletManager : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.tag != "Weapen" && !(collision.collider is SphereCollider))
            {
                Destroy(gameObject);
            }
        }
    }
}

