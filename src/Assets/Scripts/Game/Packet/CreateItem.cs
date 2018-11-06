using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateItem : MonoBehaviour {

    public GameObject bulletItem;
    public List<GameObject> arr;
    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            var index = Random.Range(0, arr.Count);
            var item = Instantiate(bulletItem, arr[index].transform);
            item.transform.localPosition = Vector3.zero;
            arr.Remove(arr[index]);
        }
    }
}
