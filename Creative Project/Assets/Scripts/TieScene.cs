using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TieScene : MonoBehaviour
{
    public int timeDelay;
    public Text t1;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UpdateText());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator UpdateText(){
        yield return new WaitForSeconds(timeDelay);
        t1.text = "Game Point!";
        yield return new WaitForSeconds(timeDelay);
        t1.text = "Good Luck...";
        yield return new WaitForSeconds(timeDelay);
        t1.text = "";
    }
}
