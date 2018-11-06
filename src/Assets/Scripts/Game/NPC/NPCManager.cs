using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Task
{
    public class NPCManager : MonoBehaviour {
        public GameObject itemPre;
        public Player.PlayerManager player;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player" && TaskState.instance.taskState == TaskState.Task.FINISH)
            {
                TaskState.instance.changeTask();
                var item = Instantiate(itemPre);
                Packet.PacketManager.instance.PickUpItem(item.transform);
                player.HPMax += 100;
                player.HP = player.HPMax;
            }
        }
        private void OnTriggerStay(Collider other)
        {
            if (other.tag == "Player" && Input.GetKeyUp(KeyCode.F) && TaskState.instance.taskState == TaskState.Task.IDLE)
            {
                TaskState.instance.changeTask();
                TaskUI.instance.ResetTask();
            }
        }
    }
}
