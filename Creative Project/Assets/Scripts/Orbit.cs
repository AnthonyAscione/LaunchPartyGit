using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Orbit : MonoBehaviour

{


    public float G;
    float force;

    Vector3 walkVector;
    public float speed;


    Vector3 direction;

    Rigidbody2D plane;
    GameObject[] planets;
    // Start is called before the first frame update
    void Start()
    {
       
        plane = GetComponent<Rigidbody2D>();
        plane.velocity = new Vector2(walkVector.x, walkVector.y) * speed;

    }

    // Update is called once per frame
    void FixedUpdate()
    {


        direction = new Vector3(0, 0, 0) + -1 * plane.transform.position; // Direction to apply the force

        direction = direction.normalized;



        walkVector = Vector3.Cross(Vector3.back, direction).normalized;



        plane.velocity = new Vector2(walkVector.x, walkVector.y) * speed;



    }
}