using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedirectionField : MonoBehaviour
{
	
	public Dictionary<int,Vector3> discreteField = new Dictionary<int, Vector3>();

	public float fieldWidth, fieldLength;
	public float samplingDensity; //points per meter
	public int raysPerPoint;
    public float redirectionPower = 1;

	//Debug
	public GameObject redirectionSphere;
    /// <summary>
    /// Valeur du champ de redirection en tout point.
    /// Interpole le champ discret.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    /// <returns></returns>

    public GameObject real_room;
    public GameObject virtual_room;

	public Vector3 this[float x, float z]{
		get {
			if(Mathf.Abs(x) > fieldWidth/2 || Mathf.Abs(z) > fieldLength / 2)
			{
				Debug.Log("(" + x.ToString() + ";" + z.ToString() + ") :Outside redirection field !");
				return Vector3.zero;
			}
			else
			{
				int W = (int)(samplingDensity * fieldWidth);
				int i = (int)((x + fieldWidth/2) * samplingDensity) + W * (int)((z + fieldLength/2) * samplingDensity);//bottom left point
				//Debug.Log("(" + x.ToString() + ";" + z.ToString() + ") --> " + i.ToString());
				///Interpolation
				Vector3 v = new Vector3(x, 0, z);
				Vector3 p1 = GetPointPosition(i + 1);
				Vector3 p2 = GetPointPosition(i + W);


				Vector3 bl = GetPointPosition(i);
				Vector3 tr = GetPointPosition(i+W+1); //Top right point
				bool bottomTriangle = (v - bl).magnitude < (v - tr).magnitude;
				Vector3 p3 = (bottomTriangle) ? bl : tr;

				float det = (p2.z - p3.z) * (p1.x - p3.x) + (p3.x - p2.x) * (p1.z - p3.z);


				float a = ((p2.z - p3.z) * (v.x - p3.x) + (p3.x - p2.x) * (v.z - p3.z)) / det;
				float b = ((p3.z - p1.z) * (v.x - p3.x) + (p1.x - p3.x) * (v.z - p3.z)) / det;
				float c = 1 - a - b;

				//Debug.Log(a.ToString() + ";" + b.ToString() + ";" + c.ToString());

				return a*discreteField[i+1] + b*discreteField[i+W] + c * discreteField[bottomTriangle ? i : i + W + 1];
			}
		}
	}

	public Vector3 GetPointPosition(int i)
	{
		int W = (int)(samplingDensity * fieldWidth);
		return new Vector3(-fieldWidth / 2 + (i-W*(i/W)) / samplingDensity, 0.1f, -fieldLength / 2 + (i/W) / samplingDensity);
	}

	private void Start()
	{
        // Activate the real room in order to calculate the right field
        virtual_room.SetActive(false);
        real_room.SetActive(true);

        CalculateField();

        real_room.SetActive(true);
        virtual_room.SetActive(true);

		//Debug pour voir le champ discret
		/*
		int i = 0;
		int W = (int)(samplingDensity * fieldWidth);
		foreach (KeyValuePair<int, Vector3> pair in discreteField)
		{

			GameObject go = Instantiate(redirectionSphere, GetPointPosition(i), Quaternion.identity);
			float intensity = Mathf.Min(pair.Value.magnitude, 1) / 1;
			go.GetComponent<Renderer>().material.color = Color.red * intensity + Color.blue * (1 - intensity);
			go.name = i.ToString();
			i++;
		}
		*/
	}

	/// <summary>
	/// Fonction qui calcule le champ en fonction des paramètres du script (samplingDensity, rayPerPoint, fieldWidth et fieldHeight)
	/// </summary>
	public void CalculateField()
	{
		int W = (int)(samplingDensity * fieldWidth);
		int L = (int)(samplingDensity * fieldLength);

		for (int i = 0; i < L; i++)
		{
			for(int j =0; j < W; j++)
			{
				Vector3 redirection = Vector3.zero;
				Vector3 origin = new Vector3(-fieldWidth / 2 + j / samplingDensity,0.1f, -fieldLength / 2 + i / samplingDensity);
				for(int k = 0; k < raysPerPoint; k++)
				{
					RaycastHit hit;
					Vector3 direction = Vector3.right * Mathf.Cos(k * 2 * Mathf.PI / raysPerPoint) + Vector3.forward * Mathf.Sin(k * 2 * Mathf.PI / raysPerPoint);
					Ray ray = new Ray(origin, direction);
					Physics.Raycast(ray, out hit);

					//Calcul
					redirection += -1 / Mathf.Pow(hit.distance,redirectionPower) * direction; 
				}

				discreteField.Add(W*i+j, redirection);

			}
		}
	}
}
