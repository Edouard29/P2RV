using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Redirecting : MonoBehaviour
{
    public GameObject virtualCamera;
    public GameObject realCamera;

    Vector3 direction, directionGame;
    float vitesse = 0.01f;
    float vitesseRot = 0.005f;

    public RedirectionField redirField;
    Vector3 champOpressif;

    FindDirection findDirection;

    // Start is called before the first frame update
    void Start()
    {
        direction = new Vector3(0.0f, 0.0f, 0.0f);
        directionGame = new Vector3(0.0f, 0.0f, 0.0f);
        findDirection = GetComponent<FindDirection>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            direction = new Vector3(0.0f, 0.0f, 1.0f);
            directionGame = direction;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            direction = new Vector3(0.0f, 0.0f, -1.0f);
            directionGame = direction;
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            direction = new Vector3(-1.0f, 0.0f, 0.0f);
            directionGame = direction;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            direction = new Vector3(1.0f, 0.0f, 0.0f);
            directionGame = direction;
        }

        direction = findDirection.direction;

        champOpressif = redirField[realCamera.transform.position.x, realCamera.transform.position.z];
        Rotate();
        Move();
    }

    private void Rotate()
    {
        // reaction du champ d'opression dans le jeu
        float angleChampOpe = Vector3.SignedAngle(direction, champOpressif, new Vector3(0.0f, 1.0f, 0.0f));
        virtualCamera.transform.Rotate(new Vector3(0.0f, - vitesseRot * direction.magnitude * angleChampOpe * champOpressif.magnitude, 0.0f));
    }

    private void Move()
    {
        // quand on bouge la rotation de la camera influe notre direction
        if (direction.magnitude > 0.001f)
        {
            /*
            // reaction du joueur
            float angle = Vector3.SignedAngle(virtualCamera.transform.forward, directionGame, new Vector3(0.0f, 1.0f, 0.0f));
            Debug.Log("2 : "+angle);
            // il bouge la tête (et le corps) pour rattraper le décalage de la caméra
            realCamera.transform.Rotate(new Vector3(0.0f, angle, 0.0f));
            virtualCamera.transform.Rotate(new Vector3(0.0f, angle, 0.0f));
            // et ainsi change légèrement sa direction
            direction = realCamera.transform.forward;
            */
            virtualCamera.transform.position += vitesse * directionGame.normalized;
            realCamera.transform.position += vitesse * direction.normalized;
        }
    }
}
