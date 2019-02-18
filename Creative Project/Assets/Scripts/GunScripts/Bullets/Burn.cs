using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burn : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }

    void OnParticleCollision(GameObject other)
    {

        if (other.gameObject.tag != "Shots")
        {
            Health health = other.GetComponent<Health>();
            if (health != null)
            { health.takeDamage(2); }


        }
    }

}
