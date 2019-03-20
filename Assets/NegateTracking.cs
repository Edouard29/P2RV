using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class NegateTracking : MonoBehaviour
{
    private void Start()
    {
        UnityEngine.XR.InputTracking.disablePositionalTracking = true;
        transform.GetChild(0).localPosition = Vector3.zero;
    }

    void Update()
    {

        transform.localRotation = Quaternion.Inverse(InputTracking.GetLocalRotation(XRNode.CenterEye));

    }
    
}
