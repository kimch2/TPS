using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using LitJson;

namespace Game.Start
{
    public class Registe : MonoBehaviour
    {
        public InputField username;
        public InputField passward;
        public InputField passward2;
        public Text usernamePrompt;
        public Text passwardPrompt;
        public Text passward2Prompt;
        
        [Serializable]
        struct Item
        {
            public string username;
            public string password;
        }
        public void ClearInput()
        {
            username.text = "";
            passward.text = "";
            passward2.text = "";
        }
        public void Confirm(GameObject openLayer)
        {
            bool canSave = true;
            Item item = new Item();
            //检测格式是否正确
            string patter = "[a-zA-Z0-9]{6,9}$";
            var regex = new Regex(patter);
            if (username.text!="" && regex.IsMatch(username.text))
            {
                item.username = username.text;
                usernamePrompt.text = "";
            }
            else
            {
                usernamePrompt.text = "用户名不合法，请输入6-10位数字或字母";
                canSave = false;
            }
            if (passward.text!="" && regex.IsMatch(passward.text))
            {
                item.password = passward.text;
                passwardPrompt.text = "";
            }
            else
            {
                passwardPrompt.text = "密码不合法，请输入6-10位数字或字母";
                canSave = false;
            }
            if (passward2.text!="" && passward.text==passward2.text)
            {
                passward2Prompt.text = "";
            }
            else
            {
                passward2Prompt.text = "两次密码不同，请输入相同密码";
                canSave = false;
            }
            //外部文件存储
            if (canSave)
            {
                List<Item> readJson;
                if (File.Exists(Application.dataPath + "/Resources/Registe.json"))
                {
                    readJson = JsonMapper.ToObject<List<Item>>(Resources.Load<TextAsset>("Registe").text);
                }
                else
                {
                    readJson = new List<Item>();
                }
                readJson.Add(item);
                var json = JsonMapper.ToJson(readJson);
                var savepath = Application.dataPath + "/Resources/Registe.json";
                File.WriteAllText(savepath, json, Encoding.UTF8);
                username.text = "";
                passward.text = "";
                passward2.text = "";
                transform.parent.GetComponent<LayerManager>().OpenLayer(openLayer);
                transform.parent.GetComponent<LayerManager>().CloseLayer(gameObject);
            }
        }
    }

}
