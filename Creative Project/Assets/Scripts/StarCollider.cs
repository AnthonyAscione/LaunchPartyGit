using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StarCollider : MonoBehaviour
{
    string t;
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
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //Destroy(col.gameObject);
        if(t == "Damage"){
            Damage(col.gameObject.name); // what it collided with; hopefully player 1 or player 2
        }

        else if(t == "Invince"){
            Gain(col.gameObject.name);
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
        if (n == "Player1" || n == "Player2")
        {
            GameObject player = GameObject.Find(n);
            Health h = player.GetComponent<Health>();
            h.GainHealth(50);
        }
    }
    

}
