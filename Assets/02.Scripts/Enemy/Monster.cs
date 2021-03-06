using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : PoolableMono, IEnemyStateMachine, IHpController
{
    [SerializeField]
    protected MonsterInfoSO monsteData;

    protected NavMeshAgent agent;
    protected Animator animator;
    private Collider bodyCollider;
    [SerializeField]
    private Collider[] hitColliders;
    private Transform nexus;
    private Transform player;

    protected Transform target;

    [SerializeField]
    private float chaseDistance;
    [SerializeField]
    private float attackDistance;


    protected int attackCnt = 0;
    protected float timer = 0f;

    private bool isDie = false;
    private bool isDamage = false;

    protected readonly int hashMove = Animator.StringToHash("move");
    protected readonly int hashAttack = Animator.StringToHash("attack");
    protected readonly int hashDie = Animator.StringToHash("die");
    protected readonly int hashAttackCount = Animator.StringToHash("attackCount");

    public EnemyState state { get; set; }
    public int MAX_HP { get; set; }
    public int currentHp { get; set; }

    void Start()
    {
        nexus = GameObject.FindWithTag("Nexus").transform;
        player = GameObject.FindWithTag("Player").transform;

        MAX_HP = monsteData.maxHp;
        currentHp = MAX_HP;
        timer = monsteData.attackDelay;
        bodyCollider = GetComponent<Collider>();
        agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = false;

        animator = GetComponent<Animator>();
        ChangeState(EnemyState.Chase);
        target = nexus;

        agent.isStopped = true;

        HitColliderEnable(0);
    }

    private void OnEnable()
    {
        StartCoroutine(StartCoroutine());
    }

    void Update()
    {
        agent.SetDestination(target.position);

        animator.SetBool(hashMove, !agent.isStopped);
        SetRotation();
    }

    void LateUpdate()
    {
        CheckState();
        MosnterAction();
    }

    IEnumerator StartCoroutine()
    {
        yield return new WaitForSeconds(1f);
        agent.isStopped = false;
    }

    private void SetRotation()
    {
        if (agent.remainingDistance >= agent.stoppingDistance)
        {
            Vector3 direction = agent.desiredVelocity;
            Quaternion rot = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * 10f);
        }
    }

    public virtual void Damage(int amount)
    {
        if (isDie == true) return;

        currentHp -= amount;

        if(target == nexus)
        {
            StartCoroutine(TargetChangeCoroutine(player));
        }

        if(currentHp <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        isDie = true;

        EnemySpawner.Instance.DeadCount();
        GoldManager.Instance.AddGold(monsteData.goldAmount);
        animator.SetTrigger(hashDie);
        StopAllCoroutines();
        agent.isStopped = true;
        bodyCollider.enabled = false;

        StartCoroutine(DestroyMonster());
    }

    IEnumerator DestroyMonster()
    {
        yield return new WaitForSeconds(1f);
        PoolManager.Instance.Push(this);
    }

    private IEnumerator TargetChangeCoroutine(Transform targetTrm)
    {
        agent.isStopped = true;
        yield return new WaitForSeconds(.5f);
        target = targetTrm;
        agent.isStopped = false;
    }

    public override void Reset()
    {
        if (agent == null)
            agent = GetComponent<NavMeshAgent>();
        if (bodyCollider == null)
            bodyCollider = GetComponent<Collider>();

        target = nexus;
        currentHp = MAX_HP;
        isDie = false;
        agent.isStopped = true;
        bodyCollider.enabled = true;
    }

    public void ChangeState(EnemyState state)
    {
        this.state = state;
    }

    public void CheckState()
    {
        if (isDie) return;

        float distance = Vector3.Distance(transform.position, target.position);

        if(distance <= attackDistance)
        {
            ChangeState(EnemyState.Attack);
        }
        else if(distance <= chaseDistance)
        {
            ChangeState(EnemyState.Chase);
        }
        else
        {
            StartCoroutine(TargetChangeCoroutine(nexus));
            ChangeState(EnemyState.Chase);
        }
    }

    public void MosnterAction()
    {
        if (isDie) return;

        switch (state)
        {
            case EnemyState.Idle:
                agent.isStopped = false;
                break;
            case EnemyState.Chase:
                agent.isStopped = false;
                break;
            case EnemyState.Attack:
                agent.isStopped = true;
                Attack();
                break;
        }
    }

    protected virtual void Attack()
    {
        if (isDie) return;

        transform.LookAt(target);

        timer += Time.deltaTime;

        if(timer >= monsteData.attackDelay)
        {
            //HitColliderEnable(1);
            attackCnt = (attackCnt + 1) % 2;
            animator.SetFloat(hashAttackCount, attackCnt);
            animator.SetTrigger(hashAttack);
            timer = 0f;
        }
    }

    void HitColliderEnable(int value)
    {
        for (int i = 0; i < hitColliders.Length; i++)
        {
            hitColliders[i].enabled = (value != 0);
        }
    }

    void IsDamageChange(int value)
    {
        isDamage = value != 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isDamage == true) return;
            IsDamageChange(1);
            other.SendMessage("Damage", monsteData.attackDamage, SendMessageOptions.DontRequireReceiver);
        }
        if (other.CompareTag("Nexus"))
        {
            if (isDamage == true) return;
            IsDamageChange(1);
            other.transform.parent.SendMessage("Damage", monsteData.attackDamage, SendMessageOptions.DontRequireReceiver);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
        Gizmos.color = Color.white;

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);
        Gizmos.color = Color.white;
    }
#endif
}
