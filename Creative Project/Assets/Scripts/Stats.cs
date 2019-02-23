using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stats : MonoBehaviour
{
    // Start is called before the first frame update
    int p1wins;
    int p2wins;
    public Text p1Text;
    public Text p2Text;

    void Start()
    {
        p1wins = 0;
        p2wins = 0;


    }

    // Update is called once per frame
    void Update()
    {
        p1Text.text = Convert.ToString(p1wins);
        p2Text.text = Convert.ToString(p2wins);

    }

    public void IncWin(string winner){
        if(winner == "Player1"){
            p1wins++;
        }
        else{
            p2wins++;
        }
    }


    private static Stats instanceRef;

    void Awake()
    {
        DontDestroyOnLoad(this);
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Stats");
        int len = objs.Length;
        if (len > 1)
        {
            Destroy(objs[1]);
        }
    }

}
