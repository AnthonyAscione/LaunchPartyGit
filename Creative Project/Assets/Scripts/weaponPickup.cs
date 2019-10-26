using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponPickup : MonoBehaviour
{
    public GameObject data;
    public int Rarity;
    public int HardCode;

    private int index;

    private GameObject gun;
    // Start is called before the first frame update
    void Start()
    {

        Set(Rarity, HardCode);





    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.gameObject.tag == "Player")
        {
            Weapons weapons = hitInfo.GetComponent<Weapons>();

            { weapons.SwitchWeapon(gun); }

            Destroy(gameObject);
        }
    }

    public void Set(int rarity, int HardCode)
    {
        ItemLists List = data.GetComponent<ItemLists>();
        switch (rarity)
        {
            case 0:
                index = Random.Range(0, 3);
                break;

            case 1:
                index = Random.Range(4, 10);
                break;

            case 2:
                index = Random.Range(8, 11);
                break;

            case 3:
                index = Random.Range(0, 3);
                break;

            case 4:
                index = HardCode;
                break;
        }


        ItemLists WeaponList = data.GetComponent<ItemLists>();
        gun = WeaponList.getWeapon(index);
        GetComponent<SpriteRenderer>().sprite = gun.GetComponent<SpriteRenderer>().sprite;
    }
    }
