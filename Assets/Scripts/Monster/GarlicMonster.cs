using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlicMonster : MonoBehaviour
{
    private GameObject playerUnit;
    public GameObject fireBall;
    private Animator thisAnimator;
    private Vector3 initialPosition;
    private AnimatorStateInfo animInfo;

    //public float wanderRadius = 0f;
    public float alertRadius;
    public float chaseRadius;

    private float defendRadius;


    private float walkSpeed;
    private float runSpeed;

    private bool isAttacking = false;
    private bool isDemaging = false;
    private bool isAttackDelay = false;
    private bool isBeatBack = false;
    private enum MonsterState
    {
        STAND,
        WALK,
        CHASE,
        RETURN,
        ATTACK,
        BEATBACK,
        TACKDEMAGE,
        DIE
    }

    private MonsterState currentState = MonsterState.STAND;

    private float distanceToPlayer;
    private float distanceToInitial;

    public bool isTakeDemage = false;
    // Start is called before the first frame update
    void Start()
    {
        playerUnit = GameObject.FindGameObjectWithTag("Player");
        thisAnimator = GetComponent<Animator>();
        initialPosition = gameObject.GetComponent<Transform>().position;

        //wanderRadius = 0f;
        alertRadius = 3f;
        chaseRadius = 5f;
        defendRadius = 1.5f;
        walkSpeed = 1f;
        runSpeed = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        animInfo = thisAnimator.GetCurrentAnimatorStateInfo(0);
        if (playerUnit != null && !playerUnit.GetComponent<PlayerState>().isDie)
        {
            isTakeDemage = GetComponent<MonsterValue>().isTakeDemage;
            PlayerDistanceCheck();
            if (isTakeDemage)
            {
                currentState = MonsterState.TACKDEMAGE;
            }
            if (this.gameObject.GetComponent<MonsterValue>().HP == 0)
            {
                currentState = MonsterState.DIE;
                thisAnimator.SetTrigger("Die");
            }
            setIdleAction(currentState);

            if (currentState == MonsterState.TACKDEMAGE && !isDemaging)
            {
                setTakeDemageAction();
            }

            //if(!isDemaging && !Attacking && currentState == MonsterState.ATTACK)
            if (animInfo.normalizedTime >= 0.95f && currentState == MonsterState.ATTACK && !isAttackDelay && distanceToPlayer < 2f)
            {
                //Invoke("setAttackAction", 1f);
                isAttackDelay = true;
                Invoke("TheAttackDelay", 2f);
                setAttackAction();
            }
        }
        else if (playerUnit.GetComponent<PlayerState>().isDie)
        {
            currentState = MonsterState.STAND;
            setIdleAction(currentState);
        }
    }

    void setTakeDemageAction()
    {
        isDemaging = true;
        thisAnimator.SetBool("IsRun", false);
        thisAnimator.SetBool("IsWalk", false);
        GetComponent<MonsterValue>().isTakeDemage = false;
        thisAnimator.SetTrigger("TakeDemage");
        currentState = MonsterState.STAND;
    }

    void setIdleAction(MonsterState theState)
    {
        switch (theState)
        {
            case MonsterState.STAND:
                thisAnimator.SetBool("IsRun", false);
                thisAnimator.SetBool("IsWalk", false);
                break;
            case MonsterState.WALK:
                thisAnimator.SetBool("IsWalk", true);
                thisAnimator.SetBool("IsRun", false);
                break;
            case MonsterState.CHASE:
                thisAnimator.SetBool("IsWalk", false);
                thisAnimator.SetBool("IsRun", true);
                transform.LookAt(playerUnit.transform.position);
                this.transform.Translate(Vector3.forward * runSpeed * Time.deltaTime);
                break;
            case MonsterState.RETURN:
                if (distanceToInitial > 0.5f)
                {
                    thisAnimator.SetBool("IsWalk", false);
                    thisAnimator.SetBool("IsRun", true);
                    transform.LookAt(initialPosition);
                    this.transform.Translate(Vector3.forward * walkSpeed * Time.deltaTime);
                }
                else
                {
                    currentState = MonsterState.STAND;
                }
                break;
        }
    }

    void setAttackAction()
    {
        transform.LookAt(playerUnit.transform.position);
        thisAnimator.SetBool("IsRun", false);
        thisAnimator.SetBool("IsWalk", false);
        if (!isAttacking)
        {
            thisAnimator.SetTrigger("Attack");
        }
    }
    void PlayerDistanceCheck()
    {
        distanceToPlayer = Vector3.Distance(playerUnit.transform.position, this.transform.position);
        distanceToInitial = Vector3.Distance(this.transform.position, initialPosition);
        if (distanceToPlayer <= defendRadius)
        {
            currentState = MonsterState.ATTACK;
        }
        else if (distanceToPlayer < alertRadius && distanceToInitial <= chaseRadius && currentState != MonsterState.RETURN)
        {
            currentState = MonsterState.CHASE;
        }
        else if (distanceToInitial > chaseRadius)
        {
            currentState = MonsterState.RETURN;
        }
    }

    public void TakeDemageToPlayer(int x, int y)
    {
        int TheDemageToPlayer = Random.Range(x, y);
        playerUnit.GetComponent<PlayerState>().HP -= TheDemageToPlayer;
        playerUnit.GetComponent<PlayerController>().isGetHit = true;
    }
    public void TheAttackDelay()
    {
        isAttackDelay = false;
    }
    //动画触发事件
    public void DieEnd()
    {
        Destroy(this.gameObject);
    }
    void TakeDemageEnd()
    {
        isDemaging = false;
    }
    void AttackEnd()
    {
        //Invoke("TheAttackDelay", 0.5f);
        //isAttacking = false;
        if (distanceToPlayer <= defendRadius)
            TakeDemageToPlayer(1, 3);
    }

    void ShootBack()
    {
        GameObject a = GameObject.Instantiate(fireBall, new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z), Quaternion.Euler(0, 0, 0));//克隆小球,初始化模型、位置、角度
        Rigidbody rgd = a.GetComponent<Rigidbody>(); //附加刚体属性
        Invoke("BeatBackHurt", 0.5f);
    }
    void BeatBackHurt()
    {
        isBeatBack = false;
        TakeDemageToPlayer(3, 5);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Bullet" && !isBeatBack)
        {
            thisAnimator.SetTrigger("BeatBack");
            transform.LookAt(playerUnit.transform);
            isBeatBack = true;
            currentState = MonsterState.STAND;
            Debug.Log("击中");
        }
    }
}
