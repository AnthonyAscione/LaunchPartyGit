using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Flame_Thrower : MonoBehaviour
{

    public Transform firingPosition;
   
    public ParticleSystem fire;
    


   





    string shootButton;
    string aimX;
    string aimY;
    Vector3 shoot;
    Vector3 md;
    Vector3 LastCoord;
    bool flipped;
    Rigidbody2D player;


    // Use this for initialization
    void Start()
    {
        
        player = GetComponentInParent<Rigidbody2D>();

       

        
        shootButton = "RT";
        aimX = "RightJoyStickX";
        aimY = "RightJoyStickY";
        //PLayer 2 settings
        if (player.name == "Player2")
        {
            shootButton = "RT_P2";
            aimX = "RightJoyStickX_P2";
            aimY = "RightJoyStickY_P2";
        }

        LastCoord.x = 1;
        LastCoord.y = 0;
        LastCoord.z = 0;



    }



    // Update is called once per frame
    void FixedUpdate()
    {
        

        // range for get axis RT on mac is -1 to 1 (usually starts at 0?)
        if (Input.GetKeyDown(KeyCode.S) || (Input.GetAxis(shootButton) >= 0.5f)) 
        {



            fire.Play();


        }
        else
        {

            fire.Stop();
        }
        md.x = Input.GetAxis(aimX);
        md.y = Input.GetAxis(aimY);
        md.z = 0;


        //when added to movement change this so default is vector of rotation of player
        if (md.x == 0 && md.y == 0) //ignore warnings this works for Not a Number
        {
            md = LastCoord;
        }

        LastCoord = md;
        float angle = Mathf.Atan2(md.y, md.x) * Mathf.Rad2Deg;
        transform.rotation = transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        float theta = Vector2.SignedAngle(player.transform.right, md);

        if (theta <= 0 && !flipped)
        {
            flip();
        }
        if (theta >= 0 && flipped)
        {
            flip();
        }
        // range for get axis RT on mac is -1 to 1 (usually starts at 0?)


    }

    void flip()
    {
        transform.Rotate(180f, 0f, 0f);

    }

  
    



    

    Vector3 UnitV(Vector3 v1)
    {
        float len = v1.magnitude;
        float x = v1.x / len;
        float y = v1.y / len;
        float z = v1.z / len;
        return new Vector3(x, y, z);
    }


    



}


