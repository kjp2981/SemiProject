using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static Define;

public class Monster : PoolableMono, IEnemyStateMachine
{
    [SerializeField]
    protected MonsterInfoSO monsteData;

    protected NavMeshAgent agent;
    protected Animator animator;
    private Collider bodyCollider;
    [SerializeField]
    private Collider[] hitColliders;

    protected Transform target;

    [SerializeField]
    private float chaseDistance;
    [SerializeField]
    private float attackDistance;

    private int hp;
    private int maxHp;

    protected int attackCnt = 0;
    protected float timer = 0f;

    private bool isDie = false;

    protected readonly int hashMove = Animator.StringToHash("move");
    protected readonly int hashAttack = Animator.StringToHash("attack");
    protected readonly int hashDie = Animator.StringToHash("die");
    protected readonly int hashAttackCount = Animator.StringToHash("attackCount");

    public EnemyState state { get; set; }

    void Start()
    {
        maxHp = monsteData.maxHp;
        hp = maxHp;
        timer = monsteData.attackDelay;
        bodyCollider = GetComponent<Collider>();
        agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = false;

        animator = GetComponent<Animator>();
        ChangeState(EnemyState.Idle);
        target = NexusTrm;

        HitColliderEnable(0);
    }

    void Update()
    {
        agent.SetDestination(target.position);

        animator.SetBool(hashMove, !agent.isStopped);
        SetRotation();
        CheckState();
        MosnterAction();
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
        hp -= amount;

        if(target == NexusTrm)
        {
            StartCoroutine(TargetChangeCoroutine(PlayerTrm));
        }

        if(hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Die");
        animator.SetTrigger(hashDie);
        agent.isStopped = true;
        bodyCollider.enabled = false;
        isDie = true;
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
        target = NexusTrm;
        hp = maxHp;
        isDie = false;
        agent.isStopped = false;
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
            StartCoroutine(TargetChangeCoroutine(NexusTrm));
            ChangeState(EnemyState.Idle);
        }
    }

    public void MosnterAction()
    {
        if (isDie) return;

        switch (state)
        {
            case EnemyState.Idle: // 약간 의도가 변경된 것 같은 느낌 일단 확실한건 이 게임에 대기 상태는 없다!
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

        Quaternion rot = Quaternion.LookRotation(target.position);
        transform.rotation = rot;

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

    private void OnTriggerEnter(Collider other)
    {
        other.SendMessage("Damage", monsteData.attackDamage, SendMessageOptions.DontRequireReceiver);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if(UnityEditor.Selection.activeGameObject == this.gameObject)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackDistance);
            Gizmos.color = Color.white;

            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
            Gizmos.color = Color.white;
        }
    }
#endif
}
