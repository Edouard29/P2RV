using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class RedirectedCamera : MonoBehaviour
{
    public GameObject realCamera;
	private ControlledCamera controlled;
	public float addedAngle;
    public RedirectionField redirectionField;
    private FindDirection realFindDirection;

    public float rotationSpeed = 0.005f;

    private void Start()
    {
		addedAngle = Vector3.SignedAngle(realCamera.transform.forward, transform.forward, Vector3.up);
        realFindDirection = realCamera.GetComponent<FindDirection>();
		controlled = realCamera.GetComponent<ControlledCamera>();
    }

    void Update()
    {
        Vector3 redirection = redirectionField[realCamera.transform.position.x, realCamera.transform.position.z];
        float angleRedirection = Vector3.SignedAngle(realFindDirection.direction, redirection, new Vector3(0.0f, 1.0f, 0.0f));
		addedAngle += -rotationSpeed * realFindDirection.direction.magnitude * angleRedirection * redirection.magnitude;

		transform.eulerAngles = realCamera.transform.eulerAngles + new Vector3(0, addedAngle, 0);

		transform.position += (Quaternion.AngleAxis(addedAngle,Vector3.up)*(controlled.transform.position - controlled.previousPosition));
		
		//transform.Translate(controlled.deltaMove);
		//transform.Rotate(controlled.deltaRotate.eulerAngles);
    }
}
