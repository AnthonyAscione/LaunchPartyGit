using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public string player;
    // Start is called before the first frame update
    void Start()
    {
        //player = "NA";
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.gameObject.tag != "Shots")
        {
            Health health = hitInfo.GetComponent<Health>();
            if (health != null)
            { health.takeDamage(20); }

            Destroy(gameObject);
        }
    }

    public string getOrigin(){
        return player;
    }

    public void setOrigin(string orig){
        player = orig;
    }
}
