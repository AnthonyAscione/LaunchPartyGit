using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RailGun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firingPosition;
    int Bullets;
    float time;
    float bspeed;
    float recoilTime;
    float lastFired;
    float charge;



    float spread;
    float gauge;


    //private AudioSource gunShot;


    public Transform point;

    string shootButton;
    string aimX;
    string aimY;
    Vector3 shoot;
    Vector3 md;
    Vector3 LastCoord;
    bool flipped;
    Rigidbody2D player;
    bool hold = false;


    // Use this for initialization
    void Start()
    {
       // gunShot = GetComponent<AudioSource>();
        player = GetComponentInParent<Rigidbody2D>();


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






        Bullets = 1;
        time = 1f;
        bspeed = 75f;
        recoilTime = 0.3f;
        spread = .15f;
        gauge = 1;


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
            hold = true;
                StartCoroutine(Ex()); //fires certain number of bullets
               
            
            
        }

        if ((Input.GetAxis(shootButton) == 0f))
        {
            hold = false;
            if (charge == 1)
                { FireGun();
            }
            charge = 0;
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

    }

    void flip()
    {
        transform.Rotate(180f, 0f, 0f);

    }

    void FireGun()
    {
      

        shoot = new Vector3(md.x, md.y, 0);
        var bullet = Instantiate(bulletPrefab, point.position, Quaternion.identity); //might only last length of function
        bullet.GetComponent<Rigidbody2D>().velocity = shoot * bspeed;
       // PlaySound(); //might have to fix later
        Destroy(bullet, 10.0f);
    }



    IEnumerator Ex()
    {
        //Vector3 md = Camera.main.ScreenToWorldPoint(Input.mousePosition); // for the mouse
        //md.z = 0; // also for the mouse





        yield return new WaitForSeconds(time);
        if (hold)
            charge = 1;
        else
            charge = 0;
    }

    Vector3 UnitV(Vector3 v1)
    {
        float len = v1.magnitude;
        float x = v1.x / len;
        float y = v1.y / len;
        float z = v1.z / len;
        return new Vector3(x, y, z);
    }

    //void PlaySound()
   // {
  //    // gunShot.Play();
 //  }



}


