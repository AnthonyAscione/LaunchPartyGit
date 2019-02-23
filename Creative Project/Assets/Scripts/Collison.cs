using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collison : MonoBehaviour
{
    public int health;
    string objectname;
    Rigidbody2D obj;

    void Start()
    {
        obj = GetComponent<Rigidbody2D>();
        objectname = obj.name;
    }

    void OnCollisionEnter2D(Collision2D col)
    {


      
         
               
                Destroy(col.gameObject);
                
            
        
    }
}