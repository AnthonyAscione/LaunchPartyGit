using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collison : MonoBehaviour
{
    public int health;
    string objectname;
    Rigidbody2D obj;

    void Start()
    {
        obj = GetComponent<Rigidbody2D>();
        objectname = obj.name;
    }

    void OnCollisionEnter2D(Collision2D col)
    {


        if (col.gameObject.name == "Black Hole")
        {
            if (objectname == "Player1" || objectname == "Player2")
            {
                //destroy object and its crosshair
                GameObject ch = GameObject.Find(objectname).GetComponent<Crosshair>().crosshair;
                Destroy(GameObject.Find(objectname));
                Destroy(ch);
            }
        }
    }
}