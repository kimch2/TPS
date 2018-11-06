using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using System.Collections.Generic;
using UnityEngine.AI;
using System.Linq;

namespace Game.AI
{
    [TaskCategory("Basic/MyAI")]
    [TaskDescription("AI追击")]
    public class Chase : Action
    {
        [BehaviorDesigner.Runtime.Tasks.Tooltip("目标对象")]
        public SharedGameObject target;
        NavMeshAgent nav;
        Transform bulletPos;
        Animator anim;
        public override void OnAwake()
        {
            nav = transform.GetComponent<NavMeshAgent>();
            var bulletObj = GameObject.FindGameObjectsWithTag("BulletParent");
            bulletObj.OrderBy(x => x.GetComponent<EnemyID>().ID);
            bulletPos = bulletObj[transform.GetComponent<EnemyID>().ID].transform;
            anim = transform.GetComponent<Animator>();
        }

        public override TaskStatus OnUpdate()
        {
            if (target == null)
            {
                return TaskStatus.Failure;
            }
            EnemyChase();
            return TaskStatus.Running;
        }
        void EnemyChase()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + Vector3.up,(target.Value.transform.position - transform.position).normalized,out hit,50f,~(1 << LayerMask.NameToLayer("Weapen"))))
            {
                if (hit.collider.tag == "Player")
                {
                    nav.isStopped = true;
                    anim.SetBool("Shot", true);
                    anim.SetFloat("WalkAngle",0);
                    float rad = Vector3.Angle(transform.forward, bulletPos.forward); //求出两向量之间的夹角 
                    Vector3 normal = Vector3.Cross(transform.forward, bulletPos.forward);//叉乘求出法线向量 
                    rad *= Mathf.Sign(Vector3.Dot(normal, transform.up));  //求法线向量与物体上方向向量点乘，结果为1或-1，修正旋转方向 
                    var dir = target.Value.transform.position - transform.position;
                    var rot = Quaternion.LookRotation(dir);
                    rot = Quaternion.Euler(rot.eulerAngles.x, rot.eulerAngles.y - rad, rot.eulerAngles.z);
                    transform.rotation = Quaternion.Lerp(transform.rotation,rot, 10);
                    return;
                }
            }
            nav.isStopped = false;
            anim.SetBool("Shot", false);
            anim.SetFloat("WalkAngle", 1);
            nav.destination = target.Value.transform.position;
        }
    }
}