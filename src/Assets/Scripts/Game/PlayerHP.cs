using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Player
{
    public class PlayerHP : MonoBehaviour
    {
        GameObject player;
        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        private void Update()
        {
            if (player)
            {
                transform.GetChild(0).localScale =Vector3.Lerp(transform.GetChild(0).localScale,new Vector3((float)player.GetComponent<PlayerManager>().HP / (float)player.GetComponent<PlayerManager>().HPMax,1,1),Time.deltaTime * 10);
                transform.GetChild(1).GetComponent<Text>().text = (float)player.GetComponent<PlayerManager>().HP + "/" + (float)player.GetComponent<PlayerManager>().HPMax;
            }
        }

    }
}

