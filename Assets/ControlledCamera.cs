using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlledCamera : MonoBehaviour
{
	public float speed = 0.03f;
	public float rotateSpeed = 1f;
	[HideInInspector]
	public Vector3 deltaMove = Vector3.zero;
	[HideInInspector]
	public Quaternion deltaRotate = Quaternion.identity;
	[HideInInspector]
	public Vector3 previousPosition = Vector3.zero;

	void Start()
	{
		previousPosition = transform.position;
	}

	void Update()
	{
		previousPosition = transform.position;

		deltaMove = speed * Vector3.forward * Input.GetAxis("Vertical");
		transform.Translate(deltaMove);

		deltaRotate = Quaternion.AngleAxis(rotateSpeed * Input.GetAxis("Horizontal"), Vector3.up);
		transform.Rotate(deltaRotate.eulerAngles);

	}
}
