using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLists : MonoBehaviour
{
    public GameObject[] weapons; // push your prefabs

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject getWeapon(int index)
    {
        return weapons[index];
    }
}
