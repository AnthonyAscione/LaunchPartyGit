using UnityEngine;
using System.Collections;

public class Weapons : MonoBehaviour
{

    public GameObject[] weapons; // push your prefabs

    public int currentWeapon = 0;

    private int nrWeapons;

    void Start()
    {

        nrWeapons = weapons.Length;
        currentWeapon = UnityEngine.Random.Range(0, nrWeapons);
        SwitchWeapon(currentWeapon); // Set default gun

    }




    void SwitchWeapon(int index)
    {
        var gun = Instantiate(weapons[index], new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        gun.transform.parent = gameObject.transform;
       
    }

}