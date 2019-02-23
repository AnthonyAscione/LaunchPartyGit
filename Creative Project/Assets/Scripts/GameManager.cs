using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    GameObject[] players;
    string winner;
    public float bound;
    public Text winnerText;
    bool gameEnded = false;
    GameObject StatManag;
    Stats stat;
    //bool endScene = false;

    void Start () {
        StatManag = GameObject.FindGameObjectWithTag("Stats");
        stat = StatManag.GetComponent<Stats>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        CheckRestart();
        CheckEscape();
        CheckPosition();
        if(Input.GetKeyDown(KeyCode.H)){
            players = GameObject.FindGameObjectsWithTag("Player");
            Destroy(players[0]);
            //gameEnded = true;
        }
	}



    void CheckRestart(){
        players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length < 2 && !gameEnded)
        {
            gameEnded = true;
            GameObject pwin = players[0];
            winner = pwin.name;
            UpdateWins(pwin);
            EndGame();
        }
    }

    void CheckEscape(){
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void EndGame()
    {
        StartCoroutine(Restart());
    }

    IEnumerator Restart(){
        string result = winner + " WINS!";
        print(result);
        winnerText.color = FindColor(winner);
        winnerText.text = result; 
        yield return new WaitForSeconds(3);
        winnerText.text = ""; //Reset Before next Scene
        int index = UnityEngine.Random.Range(0, 5);
        SceneManager.LoadScene(index);
    }

    //checks to see if a player fell off the map
    void CheckPosition(){
        players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length < 2)
        {
            CheckRestart();
        }
        else
        {
            Transform t1 = players[0].transform;
            Transform t2 = players[1].transform;
            if (Math.Abs(t1.position.x) > bound || Math.Abs(t1.position.y) > bound)
            {
                Destroy(players[0]);
            }

            if (Math.Abs(t2.position.x) > bound || Math.Abs(t2.position.y) > bound)
            {
                Destroy(players[1]);
            }
        }
    }

    void UpdateWins(GameObject o){
        string n = o.name;
        stat.IncWin(n);
    }

    Color FindColor(string s){
        if(s == "Player1"){
            return Color.red;
        }
        return Color.yellow;
    }
}
