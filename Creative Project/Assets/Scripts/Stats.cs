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
    public Slider p1Health;
    public Slider p2Health;
    public Image p1UI;
    public Image p2UI;
    public Image logo;
  
    public int WinLimit;
    Health h1;
    Health h2;
    GameObject[] players;

    Scene currScene;
    String sName;
    GameObject emptyO;
    BTS bts;

    void Start()
    {
        p1wins = 0;
        p2wins = 0;
       
    }

    // Update is called once per frame
    void Update()
    {
        

       

        emptyO = GameObject.FindGameObjectWithTag("END");
        if(emptyO){
            bts = emptyO.GetComponent<BTS>();
        }

       
        currScene = SceneManager.GetActiveScene();
        sName = currScene.name;
        if (sName == "Title")
        {
            print("title screen");
            p1UI.enabled = false;
            p2UI.enabled = false;
            logo.enabled = true;
            p2Health.enabled = false;
            p1Health.enabled = false;

        }
        else
        {
            print("not title screen");
            p1UI.enabled = true;
            p2UI.enabled = true;
            logo.enabled = false;
            p2Health.enabled = true;
            p1Health.enabled = true;
            p1Text.text = Convert.ToString(p1wins);
            p2Text.text = Convert.ToString(p2wins);
            players = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject player in players)
            {
                if (player.name == "Player1")
                {
                    h1 = player.GetComponent<Health>();
                    p1Health.value = (h1.GetHealth()); }
                else
                {
                    h2 = player.GetComponent<Health>();
                    p2Health.value = (h2.GetHealth()); }

            }


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

           
           // if(bts){
               // bts.assignV(winner);
            //}
          
            SceneManager.LoadScene(6);

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

    public void Reset()
    {
        p1wins = 0;
        p2wins = 0;
    }

   


}
