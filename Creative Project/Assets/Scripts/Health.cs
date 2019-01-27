using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void takeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        { Destroy(gameObject); }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
