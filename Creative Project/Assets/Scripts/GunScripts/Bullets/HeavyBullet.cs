using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyBullet : MonoBehaviour
{
    void Start()
    {

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
            { health.takeDamage(100); }

            Destroy(gameObject);
        }
    }
}
