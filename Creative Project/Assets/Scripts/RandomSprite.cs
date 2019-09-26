using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSprite : MonoBehaviour
   
{
    public Sprite[] planets;
    // Start is called before the first frame update
    void Start()
    {
        int index = UnityEngine.Random.Range(0, planets.Length);
        GetComponent<SpriteRenderer>().sprite = planets[index];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
