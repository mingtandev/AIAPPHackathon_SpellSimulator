﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NormalZombie : Enemy
{
    // Start is called before the first frame update



    //STATE PARTTERN
    public AttackState attack = new AttackState();
    public ChaseState chase = new ChaseState();
    public IdleState idle = new IdleState();
    public WanderState wander = new WanderState();
    public IEnemyState currentState;




    public TypeOfZombie ZombieType;

    public Animator anim;
    private float timeWalking = 0;
    private float timeChangeDirec = 4f;
    [HideInInspector]
    public bool changeDirec = true;


    public string Couroutine_PathFiding = "PathFinding";
    //private Status WalkerStatus = Status.Idle;
    public float DistanceToChase = 10f;

    protected void Awake()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        currentState = idle;
        damage = 10;
    }

    void Start()
    {
        StartCoroutine(Couroutine_PathFiding);
    }

    // Update is called once per frame
    void Update()
    {
        Action();
    }

    private void LateUpdate()
    {
        Attack();
    }

    protected void Action()
    {
        if (!isDeath)
        {
            if (heath <= 0)
            {
                StartCoroutine(DieAction());
            }

            if (ZombieType == TypeOfZombie.Walker)
                ChangeDirecWalk();
        }
    }


    void Attack()
    {
        // Debug.DrawRay(attackPoint.transform.position,-direc.normalized,Color.yellow);
        Collider[] obj = Physics.OverlapSphere(attackPoint.transform.position, 1f, LayerMask.GetMask("PlayerLayer"));
        if (obj.Length > 0)
        {
            if (obj[0].tag == "Player")
            {
                currentState = attack;
            }
        }
        else
        {
            anim.SetBool("Attack", false);

        }
    }



    void StillAttackRange()   //this function add to key event
    {
        base.AttackInRange();
    }

    IEnumerator DieAction()
    {
        anim.Play("Die");
        isDeath = true;
        StopCoroutine(Couroutine_PathFiding);
        GetComponent<NavMeshAgent>().enabled = false;  //stop path finding
        yield return new WaitForSeconds(1.8f);
        this.enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
        StartCoroutine(unactiveZombie());
    }

    IEnumerator unactiveZombie()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }

    void StopAction()
    {
        currentState = idle;
    }


    void ChangeDirecWalk()
    {
        if (timeWalking <= timeChangeDirec)
        {
            timeWalking += Time.deltaTime;
        }
        else
        {
            timeWalking = 0;
            changeDirec = true;
            //currentState = idle;
        }
    }
    IEnumerator PathFinding()
    {

        if (!player.IsDeath)
        {
            int number = Random.Range(50, 100);
            float time = 1f / number;
            float delayTime = 0;
            float realtime = 0;
            currentState = currentState.DoState(this, player, ref delayTime);


            if (delayTime + time > 1.2)
                realtime = (delayTime + time) / 2f;


            yield return new WaitForSeconds(realtime);

            if (agent == null)
            {
                yield break;

            }
            else
            {
                StartCoroutine(Couroutine_PathFiding);
            }



        }
        else
        {
            anim.SetBool("Attack", false);
            anim.SetBool("Chasing", false);
        }
    }

}
