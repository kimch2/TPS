using UnityEngine;
using System.Collections;
using DG.Tweening;
using System;

namespace Game.LoadScene
{
    public class SceneLoadingMask : MonoBehaviour
    {
        public CanvasGroup cg;
        public Transform loadingToken;
        public Vector3 initPos;

        public float duration = 0.54f;

        public void Init()
        {
            loadingToken.localPosition = initPos;
            gameObject.SetActive(false);
        }

        public void ShowMask(Action a)
        {
            loadingToken.localPosition = initPos;
            if ( !gameObject.activeInHierarchy)
            {
                gameObject.SetActive(true);
            }
            cg.alpha = 0f;
            cg.DOFade(1f, duration).OnComplete(() =>
           {
               a();
           }
            );
        }

        public void HideMask(Action a)
        {
            if (!gameObject.activeInHierarchy)
            {
                gameObject.SetActive(true);
            }
            cg.alpha = 1f;
            cg.DOFade(0f, duration).OnComplete(() =>
            {
                a();
                gameObject.SetActive(false);
            }
            );
        }

    }

}