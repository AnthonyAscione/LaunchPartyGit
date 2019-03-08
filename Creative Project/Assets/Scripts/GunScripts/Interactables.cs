using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactables : MonoBehaviour
{
    //public Text text;
    public int speed;
    public float range;
    GameObject[] players;
    bool used = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        int len = players.Length;
        if(len > 1){
            CheckUsed();
        }
    }

    void CheckUsed(){
        if(Input.GetButton("RB")){
            foreach(GameObject p in players){
                if(p.name == "Player1"){
                    CheckDistance(p); //get reference to p1
                }
            }

            }

        if(Input.GetButton("RB_P2")){
            foreach(GameObject p in players){
                if(p.name == "Player2"){
                    CheckDistance(p); // get reference to p2
                }
            }
        
        }
    }

    void CheckDistance(GameObject p){
        Transform t1 = this.transform;
        Transform t2 = p.transform;
        float distance = GetDistance(t1, t2);
        if(distance <= range){
            UseInter(p.name);
        }
    }

    float GetDistance(Transform t1, Transform t2){
        float xs = (t2.position.x - t1.position.x) * (t2.position.x - t1.position.x); // squared
        float ys = (t2.position.y - t1.position.y) * (t2.position.y - t1.position.y); // squared
        return Mathf.Sqrt(xs + ys);
    }

    void UseInter(string s){
        if(!used){
            if(s == "Player1"){
                Vector3 sp = new Vector3(0, 0, 0);
                foreach(GameObject play in players){
                    if (play.name == "Player2"){
                        sp = new Vector3(play.transform.position.x, play.transform.position.y, 0);
                        //text.text = "x " + play.transform.position.x + " y " + play.transform.position.y;
                        break;
                    }
                }

                float x = sp.x - this.transform.position.x;
                float y = sp.y - this.transform.position.y;

                Vector3 dir = new Vector3(x, y, 0);
                Vector3 ndir = dir.normalized;
                this.GetComponent<Rigidbody2D>().velocity = ndir * speed;
            }
            else{
                Vector3 sp = new Vector3(0, 0, 0);
                foreach (GameObject play in players)
                {
                    if (play.name == "Player1")
                    {
                        sp = new Vector3(play.transform.position.x, play.transform.position.y, 0);
                        //text.text = "x " + play.transform.position.x + " y " + play.transform.position.y;
                        break;
                    }
                }

                float x = sp.x - this.transform.position.x;
                float y = sp.y - this.transform.position.y;

                Vector3 dir = new Vector3(x, y, 0);
                Vector3 ndir = dir.normalized;
                this.GetComponent<Rigidbody2D>().velocity = ndir * speed;
            }
            used = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        string nam = collision.gameObject.name;
        if(used && (nam == "Player1" || nam == "Player2")){
            GameObject ch = collision.gameObject.GetComponent<Crosshair>().crosshair;
            Destroy(ch);
            Destroy(collision.gameObject);
            GameObject th = GameObject.FindGameObjectWithTag("Inter1");
            Destroy(th);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        //figure out to make interactable stay without changing the mass
    }
}
