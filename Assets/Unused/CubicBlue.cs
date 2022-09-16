using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CubicBlue : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject playerUnit;
    private Animator thisAnimator;
    AnimatorStateInfo animatorInfo;
    private Vector3 initialPosition;

    bool isReadyToExit;

    public float wanderRadius;
    public float alertRadius;
    public float defendRadius;
    public float chaseRadius;

    public float walkSpeed;
    public float runSpeed;
    public float turnSpeed;

    private bool isAttackDelay = false;
    private bool isDemageDelay = false;

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
    private Quaternion targetRotation;

    private bool is_walk = false;
    private bool is_chase = false;
    public bool isTakeDemage = false; 

    void Start()
    {
        playerUnit = GameObject.FindGameObjectWithTag("Player");
        thisAnimator = GetComponent<Animator>();
        initialPosition = gameObject.GetComponent<Transform>().position;
        walkSpeed = 1f;
        runSpeed = 2f;

}

    // Update is called once per frame
    void Update()
    {
        if(playerUnit != null)
        {
            isTakeDemage = GetComponent<MonsterValue>().isTakeDemage;
            animatorInfo = thisAnimator.GetCurrentAnimatorStateInfo(0);
            PlayerDistanceCheck();
            if (isTakeDemage)
            {
                currentState = MonsterState.TACKDEMAGE;
                //isTakeDemage = false;
            }
            if (this.gameObject.GetComponent<MonsterValue>().HP == 0)
            {
                currentState = MonsterState.DIE;
                thisAnimator.SetTrigger("Die");
                thisAnimator.SetBool("IsDie", true);
                Invoke("DestroyThisObject", 1.2f);
            }
            setIdleAction(currentState);
            if ((!isReadyToExit && animatorInfo.normalizedTime < 0.5f) || (currentState != MonsterState.STAND && currentState != MonsterState.CHASE))
            {
                isReadyToExit = true;

            }

            if (isReadyToExit)
            {
                if (currentState == MonsterState.TACKDEMAGE && !animatorInfo.IsTag("force"))
                {
                    setForceAction(currentState);
                }
                else if (animatorInfo.normalizedTime >= 0.95f)
                {
                    setAttackAction(currentState);
                }
            }
            if (animatorInfo.IsTag("attack") && animatorInfo.normalizedTime > 0.8f && !isDemageDelay && distanceToPlayer <= defendRadius)
            {
                isDemageDelay = true;
                if (animatorInfo.IsName("Attack 02"))
                {
                    playerUnit.GetComponent<PlayerState>().Debuff_1 = "Blue";
                }
                Invoke("TakeDemageToPlayer", 0.05f);
            }
        }

    }

    void setForceAction(MonsterState theState)
    {
        if (theState == MonsterState.TACKDEMAGE)
        {
            thisAnimator.SetBool("Run Forward", false);
            thisAnimator.SetBool("Walk Forward", false);
            isTakeDemage = false;
            GetComponent<MonsterValue>().isTakeDemage = false;
            thisAnimator.SetTrigger("Take Damage");
            currentState = MonsterState.STAND;
        }
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
                if(distanceToInitial > 0.5f)
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

    void setAttackAction(MonsterState theState)
    {
        if(currentState == MonsterState.ATTACK)
        {
            transform.LookAt(playerUnit.transform.position);
            thisAnimator.SetBool("Run Forward", false);
            thisAnimator.SetBool("Walk Forward", false);
            if (!isAttackDelay)
            {
                int attackType = Random.Range(0, 3);
                //Debug.Log(attackType);
                if (attackType == 0)
                {
                    thisAnimator.SetTrigger("Attack 02");
                    //Invoke("TakeDemageToPlayer", 0.5f);
                }
                else
                {
                    thisAnimator.SetTrigger("Attack 01");
                    //Invoke("TakeDemageToPlayer", 0.5f);
                }

                isAttackDelay = true;
                Invoke("TheAttackDelay", 1.2f);
            }
        }
    }
    void PlayerDistanceCheck()
    {
        distanceToPlayer = Vector3.Distance(playerUnit.transform.position, this.transform.position);
        distanceToInitial = Vector3.Distance(this.transform.position, initialPosition);

        if (distanceToPlayer < defendRadius)
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

    public void TheAttackDelay()
    {
        isAttackDelay = false;
    }
    public void TheDemageDelay()
    {
        isDemageDelay = false;
    }
    public void TakeDemageToPlayer()
    {
        int TheDemageToPlayer = Random.Range(1, 3);
        playerUnit.GetComponent<PlayerState>().HP-=TheDemageToPlayer;
        Invoke("TheDemageDelay", 0.15f);
    }

    void DestroyThisObject()
    {
        Destroy(this.gameObject);
    }
}
