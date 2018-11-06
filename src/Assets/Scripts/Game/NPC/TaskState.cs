using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Task
{
    public class TaskState : Instance.Instance<TaskState>
    {
        public SpriteRenderer spr;
        public Sprite exclamationMark;
        public Sprite questionMark;

        public Task taskState;
        public enum Task
        {
            IDLE,
            UNFINISH,
            FINISH
        }
        private new void Awake()
        {
            base.Awake();
            taskState = Task.IDLE;
            spr.sprite = exclamationMark;
            spr.color = Color.green;
        }
        public void changeTask()
        {
            switch (taskState)
            {
                case Task.IDLE:
                    taskState = Task.UNFINISH;
                    spr.sprite = questionMark;
                    spr.color = Color.white;
                    break;
                case Task.UNFINISH:
                    taskState = Task.FINISH;
                    spr.color = Color.green;
                    break;
                case Task.FINISH:
                    taskState = Task.IDLE;
                    spr.sprite = exclamationMark;
                    spr.color = Color.green;
                    break;
                default:
                    break;
            }
        }
    }
}
