using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindDirection : MonoBehaviour
{
    int numberOfAveragedFrames = 30;
    int frameCounter = 0;
    Vector3[] positions;

    
    Vector3 origin;
    Vector3 extremity;
    [HideInInspector]
    public Vector3 direction;
    

    // Start is called before the first frame update
    void Start()
    {
        positions = new Vector3[numberOfAveragedFrames];
 
    }

    // Update is called once per frame
    void Update()
    {
        positions[frameCounter] = gameObject.transform.position;

        frameCounter++;
        if(frameCounter >= numberOfAveragedFrames)
        {
            //Debug.Log(frameCounter);
            origin = positions[0];
            extremity = positions[numberOfAveragedFrames - 1];
            Debug.DrawLine(origin, extremity, new Vector4(1.0f, 0.0f, 0.0f, 1.0f), 0.5f);
            frameCounter = 0;
            direction = extremity - origin;
        }
    }
}
