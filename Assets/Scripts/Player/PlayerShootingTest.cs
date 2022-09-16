using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingTest : MonoBehaviour
{
    public GameObject Ball;
    public GameObject BornPlace;
    public bool IsShoot;
    // Start is called before the first frame update
    void Start()
    {
        IsShoot = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(IsShoot && this.GetComponent<PlayerState>().MP != 0)
        {
            GameObject a = GameObject.Instantiate(Ball, BornPlace.transform.position, Quaternion.Euler(0, 0, 0));//克隆小球,初始化模型、位置、角度
            Rigidbody rgd = a.GetComponent<Rigidbody>(); //附加刚体属性
            //Destroy(a.gameObject);
            IsShoot = false;
            this.GetComponent<PlayerState>().MP--;
        }
    }

}
