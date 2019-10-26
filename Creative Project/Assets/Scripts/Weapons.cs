using UnityEngine;
using System.Collections;

public class Weapons : MonoBehaviour
{

    public GameObject[] startWeapons; // push your prefabs

    

    int currentWeapon = 0;

   

   
    void Start()
    {

        currentWeapon = UnityEngine.Random.Range(0, 4);
        SwitchWeapon(startWeapons[currentWeapon]); // Set default gun

    }




    public void SwitchWeapon(GameObject newGun)
    {
        foreach (Transform child in transform)
        {
            GameObject obj = child.gameObject;
            if (obj.tag == "Weapon")
            { Destroy(child.gameObject); }
        }

       

        var gun = Instantiate(newGun, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        gun.transform.parent = gameObject.transform;
       
    }

}