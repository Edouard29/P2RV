using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Outil pour tester la continuité du champs.
/// On peut le bouger dans la scène et voir la valeur du champ en temps réel dans la console
/// </summary>
public class Cursor : MonoBehaviour
{
	public RedirectionField redirectionField;

    void Update()
    {
		Vector3 v = redirectionField[transform.position.x, transform.position.z];
		Debug.Log(v);
    }
}
