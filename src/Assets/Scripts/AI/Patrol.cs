using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using System.Collections.Generic;
using UnityEngine.AI;

namespace Game.AI
{
    [TaskCategory("Basic/MyAI")]
    [TaskDescription("AI巡逻")]
    public class Patrol : Action
    {
        [BehaviorDesigner.Runtime.Tasks.Tooltip("更改的目标")]
        public SharedGameObject targetGameObject;
        [BehaviorDesigner.Runtime.Tasks.Tooltip("更改动画机属性名字")]
        public SharedString paramaterName;
        [BehaviorDesigner.Runtime.Tasks.Tooltip("更改的数值")]
        public SharedFloat floatValue;

        List<Transform> navMeshPoint;
        private int hashID;
        private Animator animator;
        private GameObject prevGameObject;
        NavMeshAgent nav;
        int rand = 0;
        public override void OnAwake()
        {
            nav = transform.GetComponent<NavMeshAgent>();
        }
        public override void OnStart()
        {
            navMeshPoint = GameObject.Find("GameManager").GetComponent<NavPoint>().navMeshPoint;
            var currentGameObject = GetDefaultGameObject(targetGameObject.Value);
            if (currentGameObject != prevGameObject)
            {
                animator = currentGameObject.GetComponent<Animator>();
                prevGameObject = currentGameObject;
            }
        }
        public override TaskStatus OnUpdate()
        {
            if (animator == null)
            {
                Debug.LogWarning("Animator is null");
                return TaskStatus.Failure;
            }
            animator.SetFloat(paramaterName.Value,floatValue.Value);
            EnemyPatrol();
            return TaskStatus.Running;
        }
        void EnemyPatrol()
        {
            nav.isStopped = false;
            if (nav.remainingDistance <= nav.stoppingDistance)
            {
                rand = Random.Range(0, navMeshPoint.Count);
            }
            nav.destination = navMeshPoint[rand].position;
        }
    }
}