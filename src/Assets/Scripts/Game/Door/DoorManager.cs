using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.Door
{
    public class DoorManager : MonoBehaviour
    {

        private Vector3 startPos;
        private bool playerEnter = false;
        private void Awake()
        {
            startPos = transform.position;
        }
        private void Update()
        {
            if (playerEnter)
            {
                transform.position = Vector3.Lerp(transform.position, startPos + Vector3.up * 3, Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, startPos, Time.deltaTime);
            }
        }
        private void OnTriggerStay(Collider other)
        {
            if (other.tag=="Player")
            {
                playerEnter = true;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.tag=="Player")
            {
                playerEnter = false;
            }
        }
    }
}

