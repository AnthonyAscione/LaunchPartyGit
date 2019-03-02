using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public GameObject death;
    public int health = 100;
    Rigidbody2D player;
    GameObject Cross;
    
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
            StartCoroutine(kill());
           
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetHealth()
    {
        return health;
    }

    IEnumerator kill()
    {
        Crosshair ch = GetComponent<Crosshair>();
        Cross = ch.GetCrosshair();
        health = 0;
        yield return new WaitForSeconds(.1f);
        Destroy(Cross);
        Destroy(gameObject);


    }
    
}
