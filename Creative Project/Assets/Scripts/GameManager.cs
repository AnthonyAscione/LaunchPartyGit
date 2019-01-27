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

    void Start () {
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        CheckRestart();
        CheckEscape();
        CheckPosition();
	}



    void CheckRestart(){
        players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length < 2)
        {
            winner = players[0].name;
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

    Color FindColor(string s){
        if(s == "Player1"){
            return Color.red;
        }
        return Color.yellow;
    }
}
