﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Charge_Laser : MonoBehaviour
{

    public Transform firingPosition;
    int Bullets;
    float time;
    float bspeed;
    float recoilTime;
    float lastFired;
    public LineRenderer line;
    bool firing = false;
    

    float spread;
    float gauge;
    public float chargeLevel = 0;

    private AudioSource gunShot;




    string shootButton;
    string aimX;
    string aimY;
    Vector3 shoot;
    Vector3 md;
    bool flipped;
    Rigidbody2D player;


    // Use this for initialization
    void Start()
    {
        gunShot = GetComponent<AudioSource>();
        player = GetComponentInParent<Rigidbody2D>();
        line = GetComponentInChildren<LineRenderer>();
        line.startWidth = .005f;
        line.endWidth = .005f;


        lastFired = 0;
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







        recoilTime = .7f;

        spread = 0f;






    }



    // Update is called once per frame
    void FixedUpdate()
    {


        // range for get axis RT on mac is -1 to 1 (usually starts at 0?)
        if ((Input.GetKeyDown(KeyCode.S) || (Input.GetAxis(shootButton) >= 0.5f))&&!firing)
        {


            Ex(); //fires certain number of bullets



        }
        else
        {
            line.enabled = false;
            chargeLevel = 0;
            firing = false;
         
        }
        md.x = Input.GetAxis(aimX);
        md.y = Input.GetAxis(aimY);
        md.z = 0;

        
        //when added to movement change this so default is vector of rotation of player
        if (md.x == 0 && md.y == 0) //ignore warnings this works for Not a Number
        {
            md.x = 1;
        }
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

    void FireTracer(Vector3 v1)
    {

        RaycastHit2D hitInfo = Physics2D.Raycast(firingPosition.position, v1);


        if (hitInfo)
        {
            Debug.Log(hitInfo.transform.name);
            line.SetPosition(0, firingPosition.position);
            line.SetPosition(1, hitInfo.point);
          
        }
        else
        {
            line.SetPosition(0, firingPosition.position);
            line.SetPosition(1, md * 100);
        }
        line.enabled = true;
        

    }

    IEnumerator FireLaser(Vector3 v1)
    {
       
        RaycastHit2D hitInfo = Physics2D.Raycast(firingPosition.position, v1);
        line.startWidth = .5f;
        line.endWidth = .5f;

        if (hitInfo)
        {
            
            line.SetPosition(0, firingPosition.position);
            line.SetPosition(1, hitInfo.point);
            Health health = hitInfo.transform.GetComponent<Health>();
            if (health != null)
            { health.takeDamage(200); }
        

    }
        else
        {
            line.SetPosition(0, firingPosition.position);
            line.SetPosition(1, md * 100);
        }
        line.enabled = true;
        yield return new WaitForSeconds(.5f);
        line.enabled = false;
        line.startWidth = .005f;
        line.endWidth = .005f;
        firing = true;
        chargeLevel = 0;



    }



    void Ex()
    {
        //Vector3 md = Camera.main.ScreenToWorldPoint(Input.mousePosition); // for the mouse
        //md.z = 0; // also for the mouse

        md.x = Input.GetAxis(aimX);
        md.y = Input.GetAxis(aimY);
        md.z = 0;

        //when added to movement change this so default is vector of rotation of player
        if (md.x == 0 && md.y == 0) //ignore warnings this works for Not a Number
        {
            md.x = 1;
        }





        float xRand = Random.Range(-.1f, .1f) * spread;
        float yRand = Random.Range(-.1f, .1f) * spread;
        Vector3 dir = UnitV(new Vector3(md.x + xRand, md.y + yRand, 0));

        FireTracer(dir);
        StartCoroutine(charge());


        if (chargeLevel >= 100)
        { StartCoroutine(FireLaser(dir)); ; }


    }
    IEnumerator charge()
    {
       
        chargeLevel ++;
        yield return new WaitForSeconds(0.1f);
    }



    Vector3 UnitV(Vector3 v1)
    {
        float len = v1.magnitude;
        float x = v1.x / len;
        float y = v1.y / len;
        float z = v1.z / len;
        return new Vector3(x, y, z);
    }

    void PlaySound()
    {
        gunShot.Play();
    }



}


