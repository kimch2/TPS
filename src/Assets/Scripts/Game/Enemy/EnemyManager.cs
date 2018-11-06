using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Enemy
{
    public class EnemyManager : MonoBehaviour
    {
        public int HP;
        private void Start()
        {
           
        }
        public void Hurt(int damage)
        {
            HP -= damage;
            if (HP<=0)
            {
                HP = 0;
                Task.TaskUI.instance.changeEnemyCount();
                transform.GetComponent<BehaviorDesigner.Runtime.BehaviorTree>().GetVariable("playerInView").SetValue(false);
                gameObject.SetActive(false);
            }
        }
    }

}
