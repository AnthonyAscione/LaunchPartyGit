using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BTS : MonoBehaviour
{
    int counter = 0;
    int counter2 = 0;
    public int ec;
    public Text winner;
    GameObject StatManag;
    //string victP = "";
    Stats stat;
    // Start is called before the first frame update
    void Start()
    {
        StatManag = GameObject.FindGameObjectWithTag("Stats");
        stat = StatManag.GetComponent<Stats>();
        StartCoroutine(LoadTitleScene());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (counter2 == ec)
        {
            if (counter % 2 == 0)
            {
                winner.color = Color.yellow;
            }
            else
            {
                winner.color = Color.red;
            }
            counter++;
            counter2 = 0;
        }
        counter2++;
    }

    IEnumerator LoadTitleScene(){
        yield return new WaitForSeconds(5);
        stat.Reset();
        SceneManager.LoadScene(6); //Load Title Screen
    }

    //public void assignV(string n){
       //victP = n;
    //}
}
