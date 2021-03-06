﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed = 0.03f;
	public float rotateSpeed = 1f;
	public Vector3 deltaMove = Vector3.zero;
	public Quaternion deltaRotate = Quaternion.identity;

    void Update()
    {
        deltaMove = speed*Vector3.forward * Input.GetAxis("Vertical");
		transform.Translate(deltaMove);

		deltaRotate = Quaternion.AngleAxis(rotateSpeed * Input.GetAxis("Horizontal"), Vector3.up);
		transform.Rotate(deltaRotate.eulerAngles);
    }
}
