using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Game.Instance;
using UnityEngine.UI;
using System;

namespace Game.LoadScene
{
    // 场景加载器
    public class SceneLoader : Instance<SceneLoader>
    {
        public SceneLoadingMask mask;
        public Text text;
        public Slider sli;

        string sceneName = "";
        bool  stepFin= false;
        bool closeUpdate = false;

        AsyncOperation asy;

        public static void LoadScene(string _name)
        {
            if (instance == null)
            {
                return;
            }
            
            instance.StartCoroutine(instance.DoLoadScene(_name));
        }

        public void Start()
        {
            DontDestroyOnLoad(gameObject);
            if( mask != null )
            {
                mask.Init();
            }
        }

        private void FixedUpdate()
        {
            if (sceneName != "" && !closeUpdate)
            {
                if (asy != null)
                {
                    Debug.Log(asy.progress);
                    sli.value = Mathf.Lerp(sli.value, asy.progress*100, Time.deltaTime * 2);
                    text.text = (int)sli.value + "%";
                }
            }
        }

        IEnumerator DoLoadScene(string _name)
        {
            sceneName = _name;
            stepFin = false;
            // 第一阶段 蒙mask
            if (mask != null)
            {
                mask.ShowMask(()=> { stepFin = true; });
            }
            // mask 蒙好了
            while (!stepFin)
            {
                yield return null;
            }
            stepFin = false;
            // 换场景
            asy =  SceneManager.LoadSceneAsync(_name);

            while (!stepFin)
            {
                yield return new WaitForSeconds(1);
            }
            stepFin = false;
            // 去掉 mask
            if (mask != null)
            {
                mask.HideMask(()=> { stepFin = true; });
            }
            while (!stepFin)
            {
                yield return null;
            }
            OnSceneReady();
        }

        public static void OnSceneConstructorFinish()
        {
            instance.stepFin = true;
            instance.sli.value = 100;
            instance.text.text = (int)instance.sli.value + "%";
            instance.closeUpdate = true;
        }

        void OnSceneReady()
        {
            Destroy(gameObject);
        }
    }

}