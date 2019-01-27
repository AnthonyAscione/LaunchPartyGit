using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletFire : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firingPosition;
    public int Bullets; 
    public float time; 
    public float bspeed;
    public float recoilTime;
    public float lastFired;
    public bool burst3;
    public AudioClip shootSound;
    bool right = true;
    
    public float spread;
    public float gauge;
    public int gun;

    private AudioSource gunShot;
   
    private float volLow = 0.5f;
    private float volHigh = 1.5f;
    public Transform point;
    public Transform pt;
    string shootButton;
    string aimX;
    string aimY;
    Vector3 shoot;
    Vector3 md;


    // Use this for initialization
    void Start()
    {
         gunShot = GetComponent<AudioSource>();
        Rigidbody2D player = GetComponentInParent<Rigidbody2D>();
        
      
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


        gun = Random.Range(0, 6);

        // 3 round burst
        if (gun == 0){
            Bullets = 3;
            time = 0.05f;
            bspeed = 10;
            recoilTime = .6f;
           
            spread = .3f;
            gauge = 1;
        }
        //Shotgun
        if (gun == 1)
        {
            Bullets = 1;
            time = 0.05f;
            bspeed = 10;
            recoilTime = 1.2f;
          
            spread = 1f;
            gauge = 5;

        }
        //AR
        if (gun == 2)
        {
            Bullets = 1;
            time = 0.05f;
            bspeed = 10;
            recoilTime = 0.3f;
            spread = .15f;
            gauge = 1;
        }

        //SMG
        if (gun == 3)
        {
            Bullets = 1;
            time = 0.05f;
            bspeed = 10;
            recoilTime = 0.1f;
            
            spread = 1.2f;
            gauge = 1;
        }

        //Sniper
        if (gun == 4)
        {
            Bullets = 1;
            time = 0.05f;
            bspeed = 15;
            recoilTime = 1f;
            
            spread = 0f;
            gauge = 5;
        }

        // auto shotgun

        if (gun == 5)
        {
            Bullets = 1;
            time = 0.05f;
            bspeed = 10;
            recoilTime = .3f;
           
            spread = 1.2f;
            gauge = 3;
        }




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
        float angle = Mathf.Atan2(md.y,md.x) * Mathf.Rad2Deg;
        transform.rotation = transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        //if (md.x < 0 && right)
        //{
        //    transform.Rotate(0, 180, 0);
        //    pt.Rotate(0, 180, 0);
        //}

        //if (md.x > 0 && !right)
        //{
        //    transform.Rotate(0, 180, 0);
        //    pt.Rotate(0, 180, 0);
        //}

    }

    void FireGun(Vector3 v1){
        shoot = new Vector3(firingPosition.position.x + md.x, firingPosition.position.y + md.y, 0);
        var bullet = Instantiate(bulletPrefab, point.position, Quaternion.identity); //might only last length of function
        bullet.GetComponent<Rigidbody2D>().velocity = v1 * bspeed;
        PlaySound(); //might have to fix later
        Destroy(bullet, 2.0f);
    }

    //void FireCharge(Vector3 v1) {
    //    gunParticles.Stop();
        
      

    //    RaycastHit2D hit = Physics2D.Raycast(firingPosition, md, range, shootableMask);
    //    gunLine.enabled = true;


    //    if (hit.collider != null && hit.rigidbody == null)
    //    { gunLine.SetPosition(1, hit.point); }
    //    else if (hit.collider != null)

    //    {
    //        hit.rigidbody.AddForce(hit.normal * 20, ForceMode2D.Impulse);
    //        gunLine.SetPosition(1, hit.point);
    //    }
    //    else
    //    { gunLine.SetPosition(1, md * range); }

    //    gunAudio.Play();

    //    yield return new WaitForSeconds(effectsDisplayTime);
    //    gunLine.enabled = false;


    //}

    IEnumerator Ex(){
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

    Vector3 UnitV(Vector3 v1){
        float len = v1.magnitude;
        float x = v1.x / len;
        float y = v1.y / len;
        float z = v1.z/ len;
        return new Vector3(x, y, z);
    }

    void PlaySound(){
        gunShot.Play();
    }



}

