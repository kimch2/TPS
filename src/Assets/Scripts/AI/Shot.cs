using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using System.Collections.Generic;
using UnityEngine.AI;
using DG.Tweening;
using System.Linq;

namespace Game.AI
{
    [TaskCategory("Basic/MyAI")]
    [TaskDescription("AI射击")]
    public class Shot : Action
    {
        [BehaviorDesigner.Runtime.Tasks.Tooltip("攻击目标")]
        public SharedGameObject target;
        [BehaviorDesigner.Runtime.Tasks.Tooltip("子弹预制体")]
        public GameObject bullet;
        
        GameObject bulletPar;
        GameObject[] bulletObj;
        Transform bulletPos;
        NavMeshAgent nav;
        float shotSpeed = 1.5f;
        public override void OnAwake()
        {
            nav = transform.GetComponent<NavMeshAgent>();
                        bulletPar = GameObject.Find("BulletParent");
            bulletObj = GameObject.FindGameObjectsWithTag("BulletParent");
            bulletObj.OrderBy(x => x.GetComponent<EnemyID>().ID);
            
            bulletPos = bulletObj[transform.GetComponent<EnemyID>().ID].transform;
        }
        public override void OnStart()
        {

        }
        public override TaskStatus OnUpdate()
        {
            nav.isStopped = true;
            shotSpeed += Time.deltaTime;
            if (shotSpeed > 3f)
            {
                shotSpeed = 0f;
                shot();
            }
            return TaskStatus.Success;
        }
        void shot()
        {
            var bull = Object.Instantiate(bullet);
            bull.transform.position = bulletPos.position;
            bull.transform.parent = bulletPar.transform;
            bull.GetComponent<Rigidbody>().AddForce(((target.Value.transform.position+Vector3.up*1f-bull.transform.position)) * 1000);
            target.Value.GetComponent<Player.PlayerManager>().Hurt(Weapen.WeapenManager.instance.weapenProperty.Damage);
            Object.Destroy(bull,3);
        }
    }
}