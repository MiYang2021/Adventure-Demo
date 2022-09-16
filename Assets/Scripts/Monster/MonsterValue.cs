using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterValue : MonoBehaviour
{
    public int initHP;
    public int HP;
    public int Strength;
    public bool isTakeDemage;
    public bool farAdvantage;

    private GameObject playerUnit;
    // Start is called before the first frame update
    void Start()
    {
        HP = 5;
        initHP = 5;
        Strength = 3;
        isTakeDemage = false;
        playerUnit = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        if (HP > initHP) HP = initHP;
        if (HP < 0) HP = 0;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet" && !farAdvantage)
        {
            isTakeDemage = true;
            if(playerUnit != null)
            {
                HP -= playerUnit.GetComponent<PlayerState>().Intelligence;
            }
        }
    }

}
