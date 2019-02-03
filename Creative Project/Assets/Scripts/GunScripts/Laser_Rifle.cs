using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Laser_Rifle : MonoBehaviour
{
   
    public Transform firingPosition;
    int Bullets;
    float time;
    float bspeed;
    float recoilTime;
    float lastFired;
    public LineRenderer line;



    float spread;
    float gauge;


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
        if (Input.GetKeyDown(KeyCode.S) || (Input.GetAxis(shootButton) >= 0.5f))
        {
            if (Time.time - lastFired >= recoilTime)
            {
                StartCoroutine(Ex()); //fires certain number of bullets
                lastFired = Time.time;
            }

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

    }

    void flip()
    {
        transform.Rotate(180f, 0f, 0f);

    }

    void FireGun(Vector3 v1)
    {
       
        RaycastHit2D hitInfo = Physics2D.Raycast(firingPosition.position, v1);
        

        if (hitInfo)
        {
            Debug.Log(hitInfo.transform.name);
            line.SetPosition(0, firingPosition.position);
            line.SetPosition(1,hitInfo.point);
            Health health = hitInfo.transform.GetComponent<Health>();
            if (health != null)
            { health.takeDamage(34); }
        }
        else
        {
            line.SetPosition(0, firingPosition.position);
            line.SetPosition(1, md * 100);
        }
        line.enabled = true;
        PlaySound(); //might have to fix later
       
    }



    IEnumerator Ex()
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
                
                FireGun(dir);
             yield return new WaitForSeconds(.02f);
                line.enabled = false;
           
        

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


