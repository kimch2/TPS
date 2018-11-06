using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.CameraManager
{
    public class SmallMapFallow : MonoBehaviour
    {

        public Transform player;

        private Vector3 offset;
        private void Awake()
        {
            offset = transform.position - player.position;
        }
        private void Update()
        {
            transform.position = offset + player.position;
        }
    }
}

