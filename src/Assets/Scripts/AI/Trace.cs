using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace Game.AI
{
    [TaskCategory("Basic/MyAI")]
    [TaskDescription("敌人是否开始追踪")]
    public class Trace : Conditional
    {
        public SharedGameObject player;
        public SharedBool playerInView;

        bool playerInTri = false;
        NavMeshAgent nav;
        Animator anim;
        public override void OnAwake()
        {
            nav = transform.GetComponent<NavMeshAgent>();
            anim = transform.GetComponent<Animator>();
            player.Value = GameObject.FindGameObjectWithTag("Player");
            playerInView.Value = false;
        }
        public override TaskStatus OnUpdate()
        {
            if (playerInTri)
            {
                if (player.Value.GetComponent<Animator>().GetLayerWeight(2) == 1)
                {
                    playerInView.Value = true;
                }
            }
            //Debug.Log(player.Value.transform.position+"  "+ transform.position);
            if (Vector3.Angle(player.Value.transform.position - transform.position, transform.forward) < 55)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position + Vector3.up, (player.Value.transform.position - transform.position).normalized, out hit))
                {
                    if (hit.collider.gameObject == player.Value)
                    {
                        playerInView.Value = true;
                    }
                }
            }
            if (playerInView.Value)
            {
                return TaskStatus.Failure;
            }
            anim.SetBool("Shot", false);
            return TaskStatus.Success;
        }

        public override void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                playerInTri = true;
            }
        }

        public override void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player")
            {
                playerInTri = false;
                playerInView.Value = false;
                anim.SetBool("Shot", false);
                nav.destination = transform.position;
            }
        }
    }
}

