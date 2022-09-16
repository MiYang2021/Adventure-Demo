using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //OnGetEnemy(this.transform , 2);
    }
    //获取主角前方60度范围内,距离小于2的敌人

    //public void OnGetEnemy(Transform player, int radius, int angle)
    //public void OnGetEnemy(Transform player, int radius)
    //{
    //    //球形射线检测,得到主角半径2米范围内所有的物件
    //    Collider[] cols = Physics.OverlapSphere(player.position, radius);
    //    //判断检测到的物件中有没有Enemy
    //    if (cols.Length > 0)
    //        for (int i = 0; i < cols.Length; i++)
    //            //判断是否是怪物
    //            if (cols[i].tag.Equals("Enemy"))
    //            {
    //                Destroy(cols[i].gameObject);
    //                ////判断敌人是否在主角前方60度范围内
    //                //Vector3 dir = cols[i].transform.position - player.position;
    //                //if (Vector3.Angle(dir, player.forward) < angle)
    //                //    cols[i].GetComponent<BaseHero>().OnTakeDamage();
    //            }
    //}
    //public Transform OnGetEnemy()
    //{
    //    //获取所有敌人
    //    GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");
    //    float distance_min = 10000;    //主角与怪物的最近距离
    //    float distance = 0;            //当前怪物与主角的距离
    //    int id = 0;                    //与主角最近的怪物的编号
    //    //遍历所有敌人,计算距离并比较
    //    for (int i = 0; i < enemy.Length; i++)
    //    {
    //        if (enemy[i].activeSelf == true)
    //        {
    //            distance = Vector3.Distance(transform.position, enemy[i].transform.position);
    //            if (distance < distance_min)
    //            {
    //                //找到一个更近的
    //                distance_min = distance;
    //                id = i;
    //            }
    //        }
    //    }
    //    return enemy[id].transform;
    //}
}
