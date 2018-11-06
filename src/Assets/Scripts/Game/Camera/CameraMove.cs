using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace Game.CameraManager
{
    public class CameraMove : MonoBehaviour
    {
        public Transform player;//玩家
        public RectTransform weapenStar;//准星

        private float distance;//相机与玩家距离
        private bool firstPerson = false;//第一、三人称切换
        private float yRot;//y方向旋转度
        private Collider firstCollider;//开始射线的碰撞体，用于解决一帧内穿模的问题

        private void Awake()
        {
            distance = (transform.position - player.transform.position).magnitude;
        }
        private void FixedUpdate()
        {
            if (!GameManager.instance.stopCameraRot)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, player.transform.position - transform.position + Vector3.up * 1f, out hit))
                {
                    firstCollider = hit.collider;
                }
            }
        }
        private void Update()
        {
            if (!GameManager.instance.stopCameraRot)
            {
                if (Input.GetKeyUp(KeyCode.V))
                {
                    firstPerson = !firstPerson;
                }
            }
        }
        private void LateUpdate()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, player.transform.position - transform.position + Vector3.up * 1f, out hit))
            {
                if (hit.collider.gameObject != player.gameObject && hit.collider.tag != "Door"
                    && hit.collider.tag != "Chain" && hit.collider.tag != "Enemy" && firstCollider == hit.collider)
                {
                    distance -= 1;
                }
            }
            if (firstPerson)
            {
                distance = -0.5f;
            }
            else
            {
                distance += Input.GetAxis("Mouse ScrollWheel") * 5;
                distance = Mathf.Clamp(distance, 0.5f, 5.5f);
            }

            if (!GameManager.instance.stopCameraRot)
            {
                yRot -= Input.GetAxis("Mouse Y");
                yRot = Mathf.Clamp(yRot, -60, 60);
                transform.RotateAround(player.position, player.InverseTransformDirection(player.up), Input.GetAxis("Mouse X"));
                transform.rotation = Quaternion.Euler(yRot + 10, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            }
            var xAngle = 90 - transform.rotation.eulerAngles.x;
            var yAngle = 270 - transform.rotation.eulerAngles.y;
            var posX = distance * Mathf.Sin(xAngle * Mathf.PI / 180) * Mathf.Cos(yAngle * Mathf.PI / 180);
            var posY = distance * Mathf.Sin(xAngle * Mathf.PI / 180) * Mathf.Sin(yAngle * Mathf.PI / 180);
            var posZ = distance * Mathf.Cos(xAngle * Mathf.PI / 180);
            transform.position = new Vector3(player.transform.position.x + posX, player.transform.position.y + posZ + Vector3.up.y * 1.7f, player.transform.position.z + posY);
        }
    }
}

