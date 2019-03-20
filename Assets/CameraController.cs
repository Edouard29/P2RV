using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed = 0.01f;
    void Update()
    {
        transform.Translate(speed*Vector3.ProjectOnPlane(transform.forward,Vector3.up) * Input.GetAxis("Vertical"));
    }
}
