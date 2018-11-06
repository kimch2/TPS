using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Packet
{
    public class GridManager : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        public int itemCount;//物品数量

        Transform item;

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (transform.childCount != 0)
            {
                item = transform.GetChild(0);
                item.SetParent(transform.parent.parent.parent);
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (item)
            {
                item.transform.position = eventData.position;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (item)
            {
                if (eventData.pointerCurrentRaycast.gameObject.tag == "Grid")
                {
                    if (eventData.pointerCurrentRaycast.gameObject.transform.childCount == 0)
                    {
                        item.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
                    }
                    else
                    {
                        var tmpItem = eventData.pointerCurrentRaycast.gameObject.transform.GetChild(0);
                        tmpItem.SetParent(transform);
                        item.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
                        tmpItem.localPosition = Vector2.zero;
                    }
                    var tmp = itemCount;
                    itemCount = eventData.pointerCurrentRaycast.gameObject.GetComponent<GridManager>().itemCount;
                    eventData.pointerCurrentRaycast.gameObject.GetComponent<GridManager>().itemCount = tmp;
                }
                else
                {
                    item.SetParent(transform);
                }
                item.transform.localPosition = Vector2.zero;
                item = null;
            }
        }
    }
}

