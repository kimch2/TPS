using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.Instance
{
    public class Instance<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T instance
        {
            get;
            private set;
        }
        protected void Awake()
        {
            if (instance == null) instance = this as T;
            else Destroy(gameObject);
        }
    }
}