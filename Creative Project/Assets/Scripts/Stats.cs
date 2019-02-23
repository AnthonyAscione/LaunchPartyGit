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
    public Text fText1;
    public Text fText2;
    public int WinLimit;
    Scene currScene;
    String sName;

    void Start()
    {
        p1wins = 0;
        p2wins = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currScene = SceneManager.GetActiveScene();
        sName = currScene.name;
        if (sName != "Scene6")
        {
            p1Text.text = Convert.ToString(p1wins);
            p2Text.text = Convert.ToString(p2wins);
            fText1.text = "P1 Kills";
            fText2.text = "P2 Kills";
        }
    }

    public bool IncWin(string winner){
        if(winner == "Player1"){
            p1wins++;
        }
        else{
            p2wins++;
        }

        if(p1wins >= WinLimit || p2wins >= WinLimit){
            SceneManager.LoadScene(5);
        }

        return true;


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
