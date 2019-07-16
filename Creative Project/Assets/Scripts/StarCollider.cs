using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class StarCollider : MonoBehaviour
{
    string t;
    bool collided = false;
    GameObject StarManag;
    ShootingStar ss;
   
    // Start is called before the first frame update
    void Start()
    {
        StarManag = GameObject.FindGameObjectWithTag("SM");
        ss = StarManag.GetComponent<ShootingStar>();
        t = ss.starType();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //Destroy(col.gameObject);
        if(t == "Damage" && !collided){
            collided = true;
            Damage(col.gameObject.name); // what it collided with; hopefully player 1 or player 2
            //print("Red star collided");
        }

        else if(t == "Invince" && !collided){
            collided = true;
            if (col.gameObject.name == "standard(Clone)"){
                Gain(col.gameObject.GetComponent<Bullet>().getOrigin());
            }

            else{
                Gain(col.gameObject.name);
            }

            //print("Green star collided");
        }

        else if(t == "Party"){
            if (col.gameObject.name == "standard(Clone)" && !collided)
            {
                collided = true;
                StarManag.GetComponent<ShootingStar>().diffStars = 1;
                StarManag.GetComponent<ShootingStar>().delayTime = 0;
                StarManag.GetComponent<ShootingStar>().speed *= 3;
                //Debug.Log("storm initial");
                StarManag.GetComponent<ShootingStar>().Signal();
                //print("storm complere");
                //StarManag.GetComponent<ShootingStar>().delayTime = 6;
            }
        }

        else if(t == "Secondary" && !collided){
            collided = true;

            if(col.gameObject.name == "standard(Clone)"){
                string orig = col.gameObject.GetComponent<Bullet>().getOrigin();
                string targ = "";
                if(orig == "Player1"){
                    targ = "Player2";
                }
                else{
                    targ = "Player1";
                }

                TakeAway(targ);
            }

            //If the star hits the player directly
            else{
                TakeAway(col.gameObject.name);
            }
        }


        Destroy(this.gameObject);
        

    }

    void Damage(string n){
        if (n == "Player1" || n == "Player2")
        {
            GameObject player = GameObject.Find(n);
            Health h = player.GetComponent<Health>();
            h.takeDamage(50);
        }
    }

    void Gain(string n){
        //print("tried " + n);
        if (n == "Player1" || n == "Player2")
        {
            GameObject player = GameObject.Find(n);
            Health h = player.GetComponent<Health>();
            h.GainHealth(50);
        }
    }
    

    void TakeAway(string n){
        if(n == "Player1" || n == "Player2"){
            GameObject player = GameObject.Find(n);
            GameObject child = player.transform.GetChild(4).gameObject;
            GameObject ch = player.GetComponent<Crosshair>().crosshair;
            player.transform.GetChild(4).gameObject.SetActive(false);
            player.GetComponent<Crosshair>().crosshair.SetActive(false);
            StarManag.GetComponent<ShootingStar>().Equip(child, ch);
        }
    }

}
