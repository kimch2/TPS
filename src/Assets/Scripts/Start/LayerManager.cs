using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Start
{
    public class LayerManager : MonoBehaviour
    {
        public void OpenLayer(GameObject layer)
        {
            layer.SetActive(true);
        }
        public void CloseLayer(GameObject layer)
        {
            layer.SetActive(false);
        }
    }
}
