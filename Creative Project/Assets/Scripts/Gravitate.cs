using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravitate : MonoBehaviour
{

   
  
    public float G;
 
    
    public float modifier;



    float force;


    float minDistance;
    Vector3 minDirection;
    Vector3 direction;
    Vector3 walkVector;
    Rigidbody2D player;
    GameObject[] planets;


    // Use this for initialization
    void Start()
    {
        //Anthonys Code
        planets = GameObject.FindGameObjectsWithTag("Planet");
        player = GetComponent<Rigidbody2D>();
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Anthony Code
        minDistance = 1000000000000000;
        foreach (GameObject planet in planets)

        {

            direction = planet.transform.position + -1 * player.transform.position; // Direction to apply the force
            float distToCenter = Vector3.Distance(planet.transform.position, player.transform.position); // Distance between player and planet
            float radius = planet.GetComponent<CircleCollider2D>().radius * planet.transform.localScale.x;
            float dist = (distToCenter - radius) + modifier;
            direction = direction.normalized;


            // Each frame we want to find the closest planet. 
            if (dist < minDistance)
            {
                minDistance = dist;
                minDirection = direction;
                Debug.Log(minDistance);
                force = (G / (minDistance * minDistance)); // The force that should be applied

            }



           



        }
        player.AddForce(minDirection * force); // Adding the force to the player

        float angle = Mathf.Atan2(minDirection.y, minDirection.x) * Mathf.Rad2Deg + 90;
        player.transform.rotation = transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);



    }
}
