using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Tool;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game
{
    public class GameManager : Instance.Instance<GameManager>
    {
        public GameObject setLayer;
        public GameObject startLayer;
        public GameObject gameLayer;
        public GameObject endLayer;
        public GameObject packet;
        public Transform playerPos;
        public Slider music;
        public Slider musicEvent;
        [HideInInspector]
        public bool stopAction = false;
        [HideInInspector]
        public bool stopCameraRot = false;
        [HideInInspector]
        public Quaternion cameraRot;
        private new void Awake()
        {
            base.Awake();
            packet.SetActive(false);
            gameLayer.SetActive(false);
        }
        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Tab) && !setLayer.activeInHierarchy)
            {
                ChangeGameState();
                packet.SetActive(!packet.activeInHierarchy);
            }
            if (Input.GetKeyUp(KeyCode.BackQuote))
            {
                ChangeGameState();
            }

            if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                cameraRot = Camera.main.transform.rotation;
                stopAction = true;
            }
            if (Input.GetKeyUp(KeyCode.LeftAlt))
            {
                Camera.main.transform.rotation = cameraRot;
                stopAction = false;
            }
            if (Input.GetKeyUp(KeyCode.Escape) && !packet.activeInHierarchy)
            {
                setLayer.SetActive(!setLayer.activeInHierarchy);
                ChangeGameState();
            }
        }
        //暂停游戏，呼出鼠标
        public void ChangeGameState()
        {
            MouseManager.instance.isMouseLocked = !MouseManager.instance.isMouseLocked;
            stopAction = !stopAction;
            stopCameraRot = !stopCameraRot;
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }
            else
            {
                Time.timeScale = 0;
            }
        }
        public void StartGame()
        {
            startLayer.SetActive(!startLayer.activeInHierarchy);
            gameLayer.SetActive(!gameLayer.activeInHierarchy);
            GameObject.FindGameObjectWithTag("Player").transform.position = playerPos.position;
        }
        public void ResetScene()
        {
            SceneManager.LoadScene("Game");
        }
    }
}

