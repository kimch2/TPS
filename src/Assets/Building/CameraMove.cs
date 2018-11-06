using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {
    public float moveSpeed = 2;

    private void Update()
    {
        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * moveSpeed, 0));

    }
}
