using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float speed = 5f;
    private GameObject Enemy;
    //public GameObject cubic;
    //public GameObject player;
    public Vector3 location;
    void Start()
    {
        Enemy = null;
        location = OnGetEnemy(this.transform, 10);
        location.y = location.y + 0.5f;
        //StartCoroutine(MoveToPosition(location));
        //Debug.Log(location);
    }
    void Update()
    {
        location = OnGetEnemy(this.transform, 10);
        location.y = location.y + 0.5f;
        if (Enemy != null)
        {
            Vector3 moveTo = Enemy.transform.position;
            moveTo.y += 0.5f;
            gameObject.transform.localPosition = Vector3.MoveTowards(transform.localPosition, moveTo, speed * Time.deltaTime);
        }
    }

    IEnumerator MoveToPosition(Vector3 location)
    {
        while (gameObject.transform.localPosition != location)
        {
            gameObject.transform.localPosition = Vector3.MoveTowards(gameObject.transform.localPosition, location, speed * Time.deltaTime);
            yield return 0;
        }
    }

    Vector3 OnGetEnemy(Transform player, int radius)
    {
        //球形射线检测,得到主角半径2米范围内所有的物件
        Collider[] cols = Physics.OverlapSphere(player.position, radius);
        float distance_min = 10000;    //主角与怪物的最近距离
        float distance = 0;            //当前怪物与主角的距离
        int id = 0;                    //与主角最近的怪物的编号
        //判断检测到的物件中有没有Enemy
        if (cols.Length > 0)
        {
            for (int i = 0; i < cols.Length; i++)
            {
                if (cols[i].tag == "Enemy")
                {
                    distance = Vector3.Distance(transform.position, cols[i].transform.position);
                    if (distance < distance_min)
                    {
                        //找到一个更近的
                        distance_min = distance;
                        id = i;
                    }
                }
            }
            Debug.Log("攻击物体标签：" + cols[id].tag);
            Enemy = cols[id].gameObject;
            return cols[id].transform.position;
        }
        return player.transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("碰撞物体标签：" + collision.gameObject.tag);
        Destroy(this.gameObject);
    }


}
