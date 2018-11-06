using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using System.Linq;

namespace Game.AI
{
    [TaskCategory("Basic/MyAI")]
    [TaskDescription("敌人开始射击")]
    public class Shoot : Conditional
    {
        public SharedGameObject player;

        Animator anim;
        Transform bulletPos;
        GameObject [] EnemyGameObject;
        Transform EnemyTransform;
        EnemyID id;
        public override void OnAwake()
        {
            EnemyGameObject = GameObject.FindGameObjectsWithTag("Enemy");
            anim = transform.GetComponent<Animator>();
            var bulletObj = GameObject.FindGameObjectsWithTag("BulletParent");
            bulletObj.OrderBy(x => x.GetComponent<EnemyID>().ID);
            bulletPos = bulletObj[transform.GetComponent<EnemyID>().ID].transform;
        }
        public override TaskStatus OnUpdate()
        {
            //if (Vector3.Angle(player.Value.transform.position - transform.position, transform.forward) < 30)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position + Vector3.up,bulletPos.forward, out hit, 10f, ~(1 << LayerMask.NameToLayer("Weapen"))))
                {
                    Debug.DrawLine(transform.position + Vector3.up, hit.point, Color.red);
                    if (hit.transform.tag == "Player")
                    {
                        anim.SetLayerWeight(1, 1);
                        return TaskStatus.Failure;
                    }
                }
            }
            anim.SetLayerWeight(1, 0);
            return TaskStatus.Success;
        }
    }
}

