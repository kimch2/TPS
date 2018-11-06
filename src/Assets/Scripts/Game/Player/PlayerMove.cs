using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player
{
    public class PlayerMove : MonoBehaviour
    {
        public float speed = 1;//人物移动速度
        public int jumpSpeed;//跳跃速度

        private Animator anim;//人物动画机
        private Camera mainCam;//主摄像机
        float timer=1;
        private void Awake()
        {
            anim = GetComponent<Animator>();
            mainCam = Camera.main;
        }
        private void Update()
        {
            if (!GameManager.instance.stopCameraRot)
            {
                playerMove();
            }
        }
        void playerMove()
        {
            if (!GameManager.instance.stopAction)
            {
                timer += Time.deltaTime;
                if (Input.GetKeyDown(KeyCode.Space) && timer>=1)
                {
                    transform.GetComponent<Rigidbody>().AddForce(transform.up * jumpSpeed);
                    timer = 0;
                }
                transform.forward = new Vector3(mainCam.transform.forward.x, 0, mainCam.transform.forward.z);
                transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + Input.GetAxis("Mouse X"), 0);
                anim.SetFloat(Hashs.verAimAngle, Mathf.Clamp(anim.GetFloat(Hashs.verAimAngle) + Input.GetAxis("Mouse Y"), -60, 60));
            }
            anim.SetFloat(Hashs.horizontal, Mathf.Clamp(Input.GetAxis("Horizontal") * speed, -0.9f, 0.9f));
            anim.SetFloat(Hashs.vertical, Mathf.Clamp(Input.GetAxis("Vertical") * speed, -0.9f, 0.9f));
        }
    }
}
