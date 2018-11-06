using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class TeleportManager : MonoBehaviour
    {
        public float speed = 5;
        public List<GameObject> go;

        bool playerEnter = false;
        bool guiOpen = false;
        void Update()
        {
            transform.Rotate(new Vector3(0, 0, speed));
            if (Task.TaskState.instance.taskState != Task.TaskState.Task.IDLE)
            {
                if (speed < 20)
                {
                    if (playerEnter)
                    {
                        speed += Time.deltaTime * 5;
                    }
                    else if (speed > 5)
                    {
                        speed -= Time.deltaTime * 10;
                    }
                }
                else
                {
                    foreach (var tmp in go)
                    {
                        if (!tmp.activeInHierarchy)
                        {
                            tmp.SetActive(true);
                        }
                    }
                    playerEnter = false;
                    speed = 5;
                    GameManager.instance.StartGame();
                }
            }
            else if(playerEnter)
            {
                guiOpen = true;
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                playerEnter = true;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player")
            {
                playerEnter = false;
            }
        }
        private void OnGUI()
        {
            if (guiOpen)
            {
                GUI.TextField(new Rect(300, 500, 100, 100), "请领取任务");
                guiOpen = false;
            }
        }
    }
}

