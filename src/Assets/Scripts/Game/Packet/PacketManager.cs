using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Game.Weapen;

namespace Game.Packet
{
    public class PacketManager : Instance.Instance<PacketManager>
    {
        public GameObject weapenPacket;//武器背包格子
        public GameObject weapenShow;//武器显示界面
        public List<Transform> grid;//道具背包格子
        public Sprite[] weapenSpr;//武器图片


        void Update()
        {
            weapenPacket.GetComponent<Image>().sprite = weapenSpr[WeapenManager.instance.weapenProperty.ID - 1];
            weapenShow.transform.GetChild(0).GetComponent<Image>().sprite = weapenSpr[WeapenManager.instance.weapenProperty.ID - 1];
            weapenShow.transform.GetChild(1).GetComponent<Text>().text = WeapenManager.instance.weapenState;
            weapenShow.transform.GetChild(3).GetComponent<Text>().text = WeapenManager.instance.bulletCapacity + "/" + WeapenManager.instance.bulletPacketCap;
        }
        //捡道具
        public void PickUpItem(Transform item)
        {
            foreach (var tmp in grid)
            {
                if (tmp.childCount == 0)
                {
                    item.SetParent(tmp);
                    item.localPosition = Vector2.zero;
                    tmp.GetComponent<GridManager>().itemCount = 30;
                    item.GetChild(0).GetComponent<Text>().text = tmp.GetComponent<GridManager>().itemCount.ToString();
                    WeapenManager.instance.bulletPacketCap += 30;
                    break;
                }
            }
        }
        /// <summary>
        /// 整理背包
        /// </summary>
        //void clearUpPacket()
        //{

        //    //grid.OrderByDescending(t=>t.GetComponent<GridManager>().ID);
        //    //foreach (var item in grid)
        //    //{
        //    //    Debug.Log(item.GetComponent<GridManager>().ID);
        //    //}
        //    //for (int i = 0; i < grid.Count;i++)
        //    //{
        //    //    if (grid[i].transform.Find("Item(Clone)"))
        //    //    {
        //    //        tmpList.Add(grid[i].transform.Find("Item(Clone)").gameObject);
        //    //    }
        //    //}

        //    //tmpList.AddRange(GameObject.FindGameObjectsWithTag("item"));
        //    //tmpList.OrderBy(t => t.GetComponent<ItemManager>().ID);
        //    //foreach (var to in tmpList)
        //    //{
        //    //    Debug.Log(to.GetComponent<ItemManager>().ID);
        //    //}
        //    //for (int i = 0; i < tmpList.Count; i++)
        //    //{
        //    //    tmpList[i].transform.SetParent(grid[i].gameObject.GetComponent<RectTransform>());
        //    //    tmpList[i].transform.localPosition = Vector3.zero;
        //    //}
        //    //tmpList.Clear();
        //}
    }

}
