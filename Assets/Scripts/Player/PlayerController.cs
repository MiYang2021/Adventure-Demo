using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private string[] attacks = {"Attack_01", "Attack_04"};//两种攻击模式
    public int attack_index = 0;//攻击模式索引
    [SerializeField] private Animator m_animators;//动画控制器
    public float Rotatespeed = 1f;//旋转速度
    
    bool IsIndexChange = false;//鼠标中轴切换玩家攻击模式时按键消抖采用的变量
    bool IsDelay = false;
    bool IsAttackDelay = false;//控制玩家攻速的变量
    public bool isNegative = false;//控制玩家进入负面状态的变量
    public bool isGetHit = false;//受击间隔控制
    private bool isHitting = false;
    
    private int medicineType = 1;//喝药种类索引

    private float DistanceToPlayer;//最近的怪物与玩家的距离

    public GameObject UIcontroller;//总UI控制器，即场景中的static_UI
    private GameObject CloseEnemy;//最近的怪物

    public bool isUI;//是否有UI正在显示
    public bool setQuestion;//是否显示死亡后的问题
    void Start()
    {
        CloseEnemy = null;
        isUI = false;
        setQuestion = false;
    }
    // Update is called once per frame
    void Update()
    {
        ChangeAttackModel();
        GetEnemy();
        SetEnemyUI();
        SetDizzy();
        SetGetHit();
    }
    private void LateUpdate()
    {
        if (!isUI)//如果有UI正在显示，则无法攻击或者喝恢复药
        {
            SetAttack();
            KeyEvents();
        }
    }
    private void ChangeAttackModel()//鼠标中轴滑动切换攻击模式，其中包括一个按键消抖
    {
        if (IsIndexChange == true)
        {
            if (IsDelay == false)
            {
                Invoke("TheDelay", 0.5f);
                IsDelay = true;
            }
        }
        else if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            ChangeAttackIndex();
        }
    }
    private void ChangeAttackIndex()//改变攻击模式
    {
        attack_index = (attack_index + 1) % 2;
        IsIndexChange = true;
    }
    //获取离玩家最近的敌人
    private void GetEnemy()
    {
        Collider[] cols = Physics.OverlapSphere(this.transform.position, 0.5f);
        if (cols.Length > 0)
        {
            for (int i = 0; i < cols.Length; i++)
            {
                if (cols[i].tag == "Enemy")
                {
                    CloseEnemy = cols[i].transform.gameObject;
                }
            }
        }
    }
    //敌人足够近时显示血条
    private void SetEnemyUI()
    {
        if (CloseEnemy != null)
        {
            DistanceToPlayer = Vector3.Distance(transform.position, CloseEnemy.transform.position);
            UIcontroller.GetComponent<Static_UI>().MonsterInitHp = CloseEnemy.GetComponent<MonsterValue>().initHP;
            UIcontroller.GetComponent<Static_UI>().MonsterHp = CloseEnemy.GetComponent<MonsterValue>().HP;
            UIcontroller.GetComponent<Static_UI>().isMonsterClose = true;
        }
        else
        {
            UIcontroller.GetComponent<Static_UI>().isMonsterClose = false;

        }
        if (CloseEnemy != null)
        {
            if (CloseEnemy.GetComponent<MonsterValue>().HP == 0)
                CloseEnemy = null;
        }
    }
    //受到方形怪两种不同的debuff之后进入眩晕状态
    private void SetDizzy()
    {
        if (GetComponent<PlayerState>().isDizzy)
        {
            GetComponent<PlayerState>().isDizzy = false;
            GetComponent<PlayerState>().Debuff_1 = "";
            GetComponent<PlayerState>().Debuff_2 = "";
            isNegative = true;
            m_animators.SetTrigger("Dizzy");
        }
    }
    //攻击控制
    private void SetAttack()
    {
        if (Input.GetMouseButtonDown(0) && IsAttackDelay == false && !isNegative)
        {
            IsAttackDelay = true;
            Invoke("TheAttackDelay", 0.7f);
            m_animators.SetTrigger(attacks[attack_index]);
            if (attacks[attack_index] == "Attack_04")
            {
                Invoke("PlayerShoot", 0.5f);
            }
            else
            {
                if (CloseEnemy != null && DistanceToPlayer <= 2f)
                {
                    Vector3 dir = CloseEnemy.transform.position - this.transform.position;
                    if (Vector3.Angle(dir, this.transform.forward) < 60)
                    {
                        CloseEnemy.GetComponent<MonsterValue>().isTakeDemage = true;
                        Invoke("HitEnemyDelay", 0.5f);
                        IsAttackDelay = true;
                    }

                }
            }
        }
    }
    //受击
    private void SetGetHit()
    {
        if (!isHitting && isGetHit && !isNegative)
        {
            isHitting = true;
            isGetHit = false;
            isNegative = true;
            m_animators.SetTrigger("GetHit");
        }
    }
    //喝药控制
    private void KeyEvents()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            medicineType = 1;
            m_animators.SetTrigger("Drink");
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            medicineType = 2;
            m_animators.SetTrigger("Drink");
        }
    }
    //辅助function
    public void TheDelay()
    {
        IsIndexChange = false;
        IsDelay = false;
    }
    public void TheAttackDelay()
    {
        IsAttackDelay = false;
    }
    public void PlayerShoot()
    {
        this.GetComponent<PlayerShootingTest>().IsShoot = true;
    }
    void HitEnemyDelay()
    {
        CloseEnemy.GetComponent<MonsterValue>().HP -= this.GetComponent<PlayerState>().Strength;
    }
    //动画Event
    void Drink()
    {
        if(medicineType == 1 && this.GetComponent<PlayerState>().HPmedicine>=1)
        {
            this.GetComponent<PlayerState>().HP += 5;
            this.GetComponent<PlayerState>().HPmedicine--;
        }
        if (medicineType == 2 && this.GetComponent<PlayerState>().MPmedicine >= 1)
        {
            this.GetComponent<PlayerState>().MP += 3;
            this.GetComponent<PlayerState>().MPmedicine--;
        }
    }
    public void DizzyEnd()
    {
        isNegative = false;
    }
    void GetHitEnd()
    {
        isNegative = false;
        isHitting = false;
    }
    //死亡控制
    public void SetDieAction()
    {
        m_animators.SetTrigger("Die");
    }
    //死亡动画播放完成后弹出复活问题
    void DieEnd()
    {
        setQuestion = true;
    }
    //复活控制
    public void SetRecoverAction()
    {
        m_animators.SetTrigger("Recover");
    }
    //在存档点清除显示了且未击败的怪物血条
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "WayPoint")
        {
            CloseEnemy = null;
        }    
    }

}
