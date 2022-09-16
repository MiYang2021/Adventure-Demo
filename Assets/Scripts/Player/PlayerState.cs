using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    // Start is called before the first frame update
    public int PlayerGrade;
    public int maxHP;
    public int maxMP;
    public int HP;
    public int MP;
    public int Strength;
    //public int Speed;
    public int Intelligence;
    public int Faith;
    public string Debuff_1;
    public string Debuff_2;
    public bool isDizzy;

    public int HPmedicine;
    public int MPmedicine;
    public int coin;

    public int Points;
    public GameObject lastPoint;
    public bool isDie;
    public GameObject QuestionUI;

    void Start()
    {
        PlayerGrade = 1;
        maxHP = 1;
        maxMP = 1;
        HP = 1;
        MP = 1;
        Strength = 1;
        Intelligence = 1;
        Faith = 1;
        Debuff_1 = "";
        Debuff_2 = "";
        isDizzy = false;
        HPmedicine = 0;
        MPmedicine = 0;
        coin = 0;
        isDie = false;
        Points = 0;

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Debuff_1);
        if(Debuff_1 == "Blue" && Debuff_2 == "Red")
        {
            isDizzy = true;
            Debuff_1 = "";
            Debuff_2 = "";
        }
        if (HP > maxHP) HP = maxHP;
        if (MP > maxMP) MP = maxMP;
        if (HP < 0) HP = 0;
        if (MP < 0) MP = 0;
        if (HP == 0 && !isDie)
        {
            isDie = true;
            this.GetComponent<PlayerController>().isNegative = true;
            this.GetComponent<PlayerController>().SetDieAction();
            QuestionUI.GetComponent<Question>().QuestionChange = true;
        }

    }
    //private void OnGUI()
    //{
    //    GUILayout.BeginArea(new Rect(50, 40, 100, 100));
    //    if (GUILayout.Button("return"))
    //    {
    //        this.transform.position = lastPoint.transform.position;
    //    }
    //    GUILayout.EndArea();
    //}
    public void Recover()
    {

         maxHP = lastPoint.GetComponent<KeepPoint>().HP;
        HP = lastPoint.GetComponent<KeepPoint>().HP;
        maxMP = lastPoint.GetComponent<KeepPoint>().HP;
        MP = lastPoint.GetComponent<KeepPoint>().HP;
        this.GetComponent<PlayerController>().setQuestion = false;
        this.GetComponent<PlayerController>().SetRecoverAction();
        
    }
    void RecoverEnd()
    {
        isDie = false;
        this.GetComponent<PlayerController>().isNegative = false;
        this.transform.position = lastPoint.transform.position;
    }
}
