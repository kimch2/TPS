using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour {
    public Transform target;

    private void Update()
    {
        transform.forward = -target.forward;
    }
}
