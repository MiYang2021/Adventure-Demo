using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CubicMonster : MonoBehaviour
{
    public string MonsterType;

    private GameObject playerUnit;
    private Animator thisAnimator;
    private Vector3 initialPosition;
    private AnimatorStateInfo animInfo;

    //public float wanderRadius;
    public float alertRadius;
    public float chaseRadius;

    private float defendRadius;


    private float walkSpeed;
    private float runSpeed;

    private bool isAttacking = false;
    private bool isDemaging = false;
    private bool isAttackDelay = false;
    private enum MonsterState
    {
        STAND,
        WALK,
        CHASE,
        RETURN,
        ATTACK,
        TACKDEMAGE,
        DIE
    }

    private MonsterState currentState = MonsterState.STAND;

    private float distanceToPlayer;
    private float distanceToInitial;

    public bool isTakeDemage = false;

    void Start()
    {
        playerUnit = GameObject.FindGameObjectWithTag("Player");
        thisAnimator = GetComponent<Animator>();
        initialPosition = gameObject.GetComponent<Transform>().position;

        //wanderRadius = 0f;
        alertRadius = 3f;
        chaseRadius = 5f;
        defendRadius = 1f;
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

    }

    void setTakeDemageAction()
    {
        isDemaging = true;
        thisAnimator.SetBool("Run Forward", false);
        thisAnimator.SetBool("Walk Forward", false);
        GetComponent<MonsterValue>().isTakeDemage = false;
        thisAnimator.SetTrigger("Take Damage");
        currentState = MonsterState.STAND;
    }

    void setIdleAction(MonsterState theState)
    {
        switch (theState)
        {
            case MonsterState.STAND:
                thisAnimator.SetBool("Run Forward", false);
                thisAnimator.SetBool("Walk Forward", false);
                break;
            case MonsterState.WALK:
                thisAnimator.SetBool("Walk Forward", true);
                thisAnimator.SetBool("Run Forward", false);
                break;
            case MonsterState.CHASE:
                thisAnimator.SetBool("Walk Forward", false);
                thisAnimator.SetBool("Run Forward", true);
                transform.LookAt(playerUnit.transform.position);
                this.transform.Translate(Vector3.forward * runSpeed * Time.deltaTime);
                break;
            case MonsterState.RETURN:
                if (distanceToInitial > 0.5f)
                {
                    thisAnimator.SetBool("Walk Forward", false);
                    thisAnimator.SetBool("Run Forward", true);
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
        thisAnimator.SetBool("Run Forward", false);
        thisAnimator.SetBool("Walk Forward", false);
        if (!isAttacking)
        {
            int attackType = Random.Range(0, 3);
            if (attackType == 0)
            {
                thisAnimator.SetTrigger("Attack 02");
            }
            else
            {
                thisAnimator.SetTrigger("Attack 01");
            }

            //isAttacking = true;
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
        if(distanceToPlayer <= defendRadius)
            TakeDemageToPlayer(1, 3);
    }
    void BiteEnd()
    {
        if (distanceToPlayer <= defendRadius)
        {
            TakeDemageToPlayer(2, 4);
            if (MonsterType == "Blue")
                playerUnit.GetComponent<PlayerState>().Debuff_1 = "Blue";
            if (MonsterType == "Red")
                playerUnit.GetComponent<PlayerState>().Debuff_2 = "Red";
        }

    }
}
