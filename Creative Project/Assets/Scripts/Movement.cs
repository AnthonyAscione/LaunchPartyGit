using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    //Anthonys code // my code
    public LayerMask groundLayer;
    public float damping;
    public float G;
    public float jump;
    public float speed;
    public float maxVelocity;
    public float distance;
    public float modifier;
    float massScale;


    float force;


    float minDistance;
    Vector3 minDirection;
    Vector3 direction;
    Vector3 walkVector;
    Rigidbody2D player;
    GameObject[] planets;
    string movelr;
    string jumpbutton;

    // Use this for initialization
    void Start () {
        //Anthonys Code
        planets = GameObject.FindGameObjectsWithTag("Planet");
        player = GetComponent<Rigidbody2D>();
        movelr = "LeftJoyStickX";
        jumpbutton = "A";
        if (player.name == "Player2")
        {
            movelr = "LeftJoyStickX_P2";
            jumpbutton = "A_P2";
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {
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
                force = (G /(minDistance * minDistance)); // The force that should be applied

            }



            walkVector = Vector3.Cross(Vector3.back, minDirection).normalized;



        }
        player.AddForce(minDirection * force); // Adding the force to the player




        if (Input.GetKey(KeyCode.RightArrow) || Input.GetAxis(movelr) > 0f)

        {
            if (player.velocity.magnitude < maxVelocity)
            {
                player.AddRelativeForce(Vector2.up * speed);
            }
           

        }
        else
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetAxis(movelr) < 0f)

        {
            if (player.velocity.magnitude < maxVelocity)
            {
                player.AddRelativeForce(Vector2.down * speed);
            }
          

        }
        else
        if (IsGrounded())
        {
            player.velocity *= damping;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButton(jumpbutton))
        { jumpCommand(); }


        float angle = Mathf.Atan2(minDirection.y, minDirection.x) * Mathf.Rad2Deg;
        player.transform.rotation = transform.rotation = Quaternion.AngleAxis(angle , Vector3.forward);
    }

    //Anthonys Code
    bool IsGrounded()
    {
        Vector2 position = transform.position;
        Vector2 direction = minDirection;


        Debug.DrawRay(position, direction * distance, Color.blue);
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);

        if (hit.collider != null)
        {

            return true;
        }

        return false;
    }

    void jumpCommand()
    {
        if (IsGrounded())

        {

            player.AddRelativeForce(Vector2.right * -jump,ForceMode2D.Impulse);
            //player.velocity = new Vector2(2 * minDirection.x - player.velocity.x, 2 * minDirection.y - player.velocity.y).normalized * -jump;

        }
    }
}
