﻿using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShootingStar : MonoBehaviour
{

    public int diffStars;
    public int speed;
    public float lifeSpan;
    public int delayTime;
    public GameObject shootStar;
    string type;
    string sName;
    float distance = 18f;
    float lastFired;
    Pair[] pairs = new Pair[6];
    // Start is called before the first frame update
    void Start()
    {
        lastFired = 0;
        type = "";
        pairs[0] = new Pair(Color.red, "Damage");
        pairs[1] = new Pair(Color.green, "Invince");
        pairs[2] = new Pair(Color.yellow, "Guns");
        pairs[3] = new Pair(Color.magenta, "Assist");
        pairs[4] = new Pair(Color.blue, "Secondary");
        pairs[5] = new Pair(Color.white, "Party");
        diffStars = pairs.Length;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        sName = SceneManager.GetActiveScene().name;
        if (sName != "Title" && sName != "Kobe")
        {
            if (Time.time - lastFired >= delayTime)
            {
                GenStar();
                lastFired = Time.time;
            }
        }

    }

    void GenStar()
    {
        float theta = UnityEngine.Random.Range(0f, 360f);
        float randx = distance * Mathf.Sin(theta);
        float randy = distance * Mathf.Cos(theta);
        Vector3 pos = new Vector3(randx, randy, 0);
        var star = Instantiate(shootStar, pos, Quaternion.identity);
        //star.GetComponent<SpriteRenderer>().color = FindColor();

        //Send it to a random location on screen
        float randDestx = UnityEngine.Random.Range(-20f, 20f);
        float randDesty = UnityEngine.Random.Range(-14f, 14f);


        Vector3 dir = new Vector3(randDestx - randx, randDesty - randy, 0);
        Vector3 udir = dir.normalized * speed;
        star.GetComponent<Rigidbody2D>().velocity = udir;
        Destroy(star, lifeSpan);
    }




    void Awake()
    {
        DontDestroyOnLoad(this);
        GameObject[] objs = GameObject.FindGameObjectsWithTag("SM");
        int len = objs.Length;
        if (len > 1)
        {
            Destroy(objs[1]);
        }


    }

    private void OnLevelWasLoaded(int level)
    {
        delayTime = 6;
        diffStars = pairs.Length;
        speed = 4;
    }

    Color FindColor()
    {
        int num = UnityEngine.Random.Range(0, diffStars); // the diffstars is not actually inclusive
        type = pairs[num].type1;
        return pairs[num].c1;
    }

    public string starType(){
        return type;
    }

    public void Signal(){
        StartCoroutine(EndStorm());
    }



    IEnumerator EndStorm(){
        yield return new WaitForSeconds(5);
        delayTime = 6;
        diffStars = pairs.Length;
        speed = 4;
        //Debug.Log("Storm final");
    }

    public void Equip(GameObject c, GameObject h){
        StartCoroutine(DelayEquip(c, h));
    }

    IEnumerator DelayEquip(GameObject c, GameObject h){
        //Debug.Log("equiping " + c.name);
        yield return new WaitForSeconds(4);
        c.SetActive(true);
        h.SetActive(true);
    }

}

public class Pair{
    public Color c1;
    public string type1;

    public Pair(Color c, string t){
        c1 = c;
        type1 = t;
    }
}
