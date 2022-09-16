using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    float currentvelocity;//用于满足API条件的特殊变量
    public float smoothtime = 0.5f;//转向速度
    public float walkspeed = 1f;//行走速度
    private Animator ac;//动画控制器
    private Transform cameratransform;//相机位置变换器
    private Rigidbody rb;//自身刚体

    private float forward_or_back;//向前向后控制变量
    // Start is called before the first frame update
    void Start()
    {
        ac = this.GetComponent<Animator>();
        cameratransform = Camera.main.transform;
        rb = this.GetComponent<Rigidbody>();
        forward_or_back = 0;
    }
    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Rigidbody>().freezeRotation = true;//将玩家自由旋转锁定，防止因为特殊碰撞进入自转状态

        if (GetComponent<PlayerController>().isNegative == false)//如果玩家处于负面状态则禁止移动
        {
            forward_or_back = Input.GetAxisRaw("Vertical");//前后方向键控制移动方向

            if (Input.GetKey(KeyCode.Space) && forward_or_back > 0)//控制跑步
            {
                forward_or_back = forward_or_back * 2;
            }
            if (forward_or_back > 1)
                ac.SetBool("IsRun?", true);
            else
                ac.SetBool("IsRun?", false);
            //由轴旋转角度控制玩家旋转，由主相机朝向为旋转参考方向
            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
            Vector2 inputdir = input.normalized;
            float targetrotation = Mathf.Atan2(inputdir.x, inputdir.y) * Mathf.Rad2Deg + cameratransform.eulerAngles.y;
            if (inputdir != Vector2.zero || Input.GetAxisRaw("Vertical") != 0)
            {
                transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetrotation, ref currentvelocity, smoothtime);
                rb.MovePosition(transform.position + transform.forward * Time.deltaTime * walkspeed * forward_or_back);
                ac.SetBool("IsMove?", true);
            }
            else
            {
                ac.SetBool("IsMove?", false);
            }
        }

    }

}
