using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Task
{
    public class TaskUI : Instance.Instance<TaskUI>
    {
        public Text text;
        public GameObject door;

        int enemyCount;
        int deadEnemyCount;
        private new void Awake()
        {
            base.Awake();
            enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        }
        public void changeEnemyCount()
        {
            deadEnemyCount++;
            text.text = "消灭敌人：" + deadEnemyCount + "/" + enemyCount;
            if (deadEnemyCount == enemyCount)
            {
                text.color = Color.green;
                text.text += "\n踏上传送阵，逃离这个地牢吧";
                door.SetActive(false);
                TaskState.instance.changeTask();
            }
        }
        public void ResetTask()
        {
            deadEnemyCount = 0;
            text.text = "消灭敌人：" + deadEnemyCount + "/" + enemyCount;
            text.color = Color.yellow;
        }
    }
}

