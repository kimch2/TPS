using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Tool
{
    public class MouseManager : Instance.Instance<MouseManager>
    {
        public bool isMouseLocked = true;
        
        void Update()
        {
            if (isMouseLocked)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
}

