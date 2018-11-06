using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemy : MonoBehaviour {
    public GameObject enemy;

    List<Transform> navPoint;
    List<int> arr = new List<int> {0,1,2,3,4,5,6,7,8,9,10,11,12,13 };
    private void Awake()
    {
        navPoint = GetComponent<NavPoint>().navMeshPoint;
        for (int i = 0; i < 4; i++)
        {
            var index = Random.Range(0, arr.Count);
            var enemyTmp = Instantiate(enemy);
            enemyTmp.transform.position = navPoint[arr[index]].position;
            arr.Remove(arr[index]);
        }
    }
}
