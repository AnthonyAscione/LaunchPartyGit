using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public GameObject death;
    public int health = 100;
    Rigidbody2D player;
    // Start is called before the first frame update
    void Start()
    {
       player = GetComponent<Rigidbody2D>();
       
    }

    public void takeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            
            Destroy(gameObject);
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
