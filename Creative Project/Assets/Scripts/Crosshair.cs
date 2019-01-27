using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Crosshair : MonoBehaviour
{

    Transform origin;
    Vector2 direction;
    public float dist;
    Vector3 coord;
    public GameObject crosshair;
    Vector3 LastCoord;
    string player;
    string aimX;
    string aimY;
    Vector2 spot;



    // Start is called before the first frame update
    void Start()
    {
        origin = gameObject.transform;
        player = GetComponent<Rigidbody2D>().name;

        aimX = "RightJoyStickX";
        aimY = "RightJoyStickY";
        crosshair.GetComponent<SpriteRenderer>().color = Color.red;

        if (player == "Player2")
        {
            aimX = "RightJoyStickX_P2";
            aimY = "RightJoyStickY_P2";
            Color c1 = new Color(1f, 1f, 0.0f, 255f); //means 255,255,0,255 
            crosshair.GetComponent<SpriteRenderer>().color = c1;
        }

        spot = new Vector2(1 + origin.position.x, origin.position.y) * dist;
        crosshair = Instantiate(crosshair, spot, Quaternion.identity);
        crosshair.GetComponent<SpriteRenderer>().sortingOrder = 1;

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        direction.x = Input.GetAxis(aimX);
        direction.y = Input.GetAxis(aimY);
        if (direction.x == 0 && direction.y == 0)
        {
            direction.x = 1;
        }

        direction = direction.normalized * dist;
        coord.x = direction.x + origin.position.x;
        coord.y = direction.y + origin.position.y;

        crosshair.transform.SetPositionAndRotation(coord, Quaternion.identity);

    }

  
}