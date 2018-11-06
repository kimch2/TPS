using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Game.End
{
    public class EndLayerManager : Instance.Instance<EndLayerManager>
    {
        public Text text;
        public Image BG;
        public GameObject myReturn;
        public GameObject exit;
        public GameObject endLayer;

        public void EndGame()
        {
            endLayer.SetActive(true);
            BG.DOFade(1f, 0.5f).OnComplete(() => text.transform.DOJump(new Vector2(Screen.width / 2, Screen.height / 2), 50, 3, 3).OnComplete(()=> 
            {
                myReturn.SetActive(true);
                exit.SetActive(true);
                GameManager.instance.ChangeGameState();
            }));
        }
    }
}

