using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class RedirectedCamera : MonoBehaviour
{
    public GameObject realCamera;
    public RedirectionField redirectionField;
    private FindDirection realFindDirection;

    public float rotationSpeed = 0.005f;

    private void Start()
    {
        realFindDirection = realCamera.GetComponent<FindDirection>();
    }

    void Update()
    {
        Vector3 redirection = redirectionField[realCamera.transform.position.x, realCamera.transform.position.z];
        float angleRedirection = Vector3.SignedAngle(realFindDirection.direction, redirection, new Vector3(0.0f, 1.0f, 0.0f));
        transform.localRotation = InputTracking.GetLocalRotation(XRNode.CenterEye);
        transform.Rotate(new Vector3(0.0f, -rotationSpeed * realFindDirection.direction.magnitude* angleRedirection * redirection.magnitude, 0.0f));
        
    }
}
