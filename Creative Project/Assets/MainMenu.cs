using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    GameObject StatManag;
    Stats stat;

    public void LoadByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void PlayGame ()
    {
        stat.LoadNextScene();
    }



    void Start()
    {
        StatManag = GameObject.FindGameObjectWithTag("Stats");
        stat = StatManag.GetComponent<Stats>();
    }
}
