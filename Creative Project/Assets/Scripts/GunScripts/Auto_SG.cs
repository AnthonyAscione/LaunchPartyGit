using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Auto_SG : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firingPosition;
    int Bullets;
    float time;
    float bspeed;
    float recoilTime;
    float lastFired;
    GameObject copPF;
    


    float spread;
    float gauge;


    private AudioSource gunShot;


    public Transform point;

    string shootButton;
    Vector3 LastCoord;
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
        time = 0.05f;
        bspeed = 20;
        recoilTime = .3f;

        spread = 1.2f;
        gauge = 3;

        LastCoord.x = 1;
        LastCoord.y = 0;
        LastCoord.z = 0;


        copPF = bulletPrefab;
        copPF.GetComponent<Bullet>().setOrigin(player.name);


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

    void FireGun(Vector3 v1)
    {
        shoot = new Vector3(firingPosition.position.x + md.x, firingPosition.position.y + md.y, 0);
        copPF.GetComponent<Bullet>().setOrigin(player.name);
        var bullet = Instantiate(copPF, point.position, Quaternion.identity); //might only last length of function
        //bullet.GetComponent<Bullet>().setOrigin(player.name); //this line allows us to track who shot what
        bullet.GetComponent<Rigidbody2D>().velocity = v1 * bspeed;
        PlaySound(); //might have to fix later
        Destroy(bullet, 2.0f);
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
            md = LastCoord;
        }

        LastCoord = md;


        for (int i = 0; i < Bullets; i++)
        {
            for (int j = 0; j < gauge; j++)
            {
                float xRand = Random.Range(-.1f, .1f) * spread;
                float yRand = Random.Range(-.1f, .1f) * spread;
                Vector3 dir = UnitV(new Vector3(md.x + xRand, md.y + yRand, 0));
                FireGun(dir);
            }
            yield return new WaitForSeconds(time);
        }

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


