using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;

namespace Game.Start
{
    public class Login : MonoBehaviour
    {

        public InputField username;
        public InputField passward;
        public Text prompt;

        [Serializable]
        struct Item
        {
            public string username;
            public string password;
        }
        List<Item> json;
        public void LoginJudge()
        {
            if (!File.Exists(Application.dataPath + "/Resources/Registe.json"))
            {
                prompt.text = "用户名或密码不存在";
                return;
            }
            bool haveUsername = false;
            json = LitJson.JsonMapper.ToObject<List<Item>>(Resources.Load<TextAsset>("Registe").text);
            foreach (var item in json)
            {
                if (item.username == username.text && item.password == passward.text)
                {
                    prompt.text = "";
                    haveUsername = true;
                    //切换到开始场景
                    gameObject.SetActive(false);
                    transform.parent.Find("BG").gameObject.SetActive(false);
                    LoadScene.SceneLoader.LoadScene("Game");
                    break;
                }
            }
            if (haveUsername == false)
            {
                prompt.text = "用户名或密码错误";
                username.text = "";
                passward.text = "";
            }
        }
    }
}

