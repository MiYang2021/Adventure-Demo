using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepPoint : MonoBehaviour
{
    // Start is called before the first frame update
    public int PointId;

    public GameObject UI;
    //记录的玩家信息
    public int Grade;
    public int HP;
    public int MP;
    public int Strength;
    //public int Speed;
    public int Intelligence;
    public int Faith;
    public string Debuff_1;
    public string Debuff_2;
    public bool isDizzy;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("keep playerState");
            collision.gameObject.GetComponent<PlayerState>().lastPoint = this.gameObject;
            collision.gameObject.GetComponent<PlayerController>().isUI = true;
            UI.gameObject.GetComponent<Static_UI>().SetKeepPonintUI = true;
            UI.gameObject.GetComponent<Static_UI>().elseUI = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        collision.gameObject.GetComponent<PlayerState>().HP = collision.gameObject.GetComponent<PlayerState>().maxHP;
        collision.gameObject.GetComponent<PlayerState>().MP = collision.gameObject.GetComponent<PlayerState>().maxMP;
        HP = collision.gameObject.GetComponent<PlayerState>().maxHP;
        MP = collision.gameObject.GetComponent<PlayerState>().maxMP;
        Grade = collision.gameObject.GetComponent<PlayerState>().PlayerGrade;
        Strength = collision.gameObject.GetComponent<PlayerState>().Strength;
        Intelligence = collision.gameObject.GetComponent<PlayerState>().Intelligence;
        UI.gameObject.GetComponent<Static_UI>().SetKeepPonintUI = false;
        UI.gameObject.GetComponent<Static_UI>().elseUI = false;
        collision.gameObject.GetComponent<PlayerController>().isUI = false;
    }

}
