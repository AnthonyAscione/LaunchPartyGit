using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleUI : MonoBehaviour
{
    Scene currScene;
    string sName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currScene = SceneManager.GetActiveScene();
        sName = currScene.name;
        //try to delete in here maybe
    }

    void Awake()
    {
        DontDestroyOnLoad(this);
        GameObject[] objs = GameObject.FindGameObjectsWithTag("TUI");
        int len = objs.Length;
        if (len > 1)
        {
            Destroy(objs[1]);
        }
    }

}
