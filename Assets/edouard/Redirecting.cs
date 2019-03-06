using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Redirecting : MonoBehaviour
{
    public GameObject game;
    public GameObject reality;
    public GameObject redirFieldObj;

    Vector3 direction, directionGame;
    float vitesse = 0.01f;
    float vitesseRot = 0.005f;

    RedirectionField redirField;
    Vector3 champOpressif;

    // Start is called before the first frame update
    void Start()
    {
        direction = new Vector3(0.0f, 0.0f, 0.0f);
        directionGame = new Vector3(0.0f, 0.0f, 0.0f);

        redirField = redirFieldObj.GetComponent<RedirectionField>();
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

        champOpressif = redirField[reality.transform.position.x, reality.transform.position.z];
        Rotation();
        Move();
    }

    private void Rotation()
    {
        // reaction du champ d'opression dans le jeu
        float angleChampOpe = Vector3.SignedAngle(direction, champOpressif, new Vector3(0.0f, 1.0f, 0.0f));
        game.transform.Rotate(new Vector3(0.0f, - vitesseRot * direction.magnitude * angleChampOpe * champOpressif.magnitude, 0.0f));
    }

    private void Move()
    {
        // quand on bouge la rotation de la camera influe notre direction
        if (direction.magnitude != 0)
        {
            // reaction du joueur
            float angle = Vector3.SignedAngle(game.transform.forward, directionGame, new Vector3(0.0f, 1.0f, 0.0f));
            Debug.Log("2 : "+angle);
            // il bouge la tête (et le corps) pour rattraper le décalage de la caméra
            reality.transform.Rotate(new Vector3(0.0f, angle, 0.0f));
            game.transform.Rotate(new Vector3(0.0f, angle, 0.0f));
            // et ainsi change légèrement sa direction
            direction = reality.transform.forward;

            game.transform.position += vitesse * directionGame.normalized;
            reality.transform.position += vitesse * direction.normalized;
        }
    }
}
