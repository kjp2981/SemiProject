using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour, IHpController, IKnockback
{
    private Animator animator;
    private CharacterController cc;
    private CollisionFlags cf = CollisionFlags.None;

    [SerializeField]
    private PlayerInfoSO playerData;
    [SerializeField]
    private Transform spineTrm;

    private float yVelocity;

    private bool isJump = false;

    private readonly int hashH = Animator.StringToHash("h");
    private readonly int hashV = Animator.StringToHash("v");
    private readonly int hashJump = Animator.StringToHash("jump");

    public int MAX_HP { get; set; }
    public int currentHp { get; set; }

    void Start()
    {
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        MAX_HP = playerData.maxHp;
        currentHp = MAX_HP;
    }

    void Update()
    {
        Move();

        Vector3 pos = MainCam.transform.eulerAngles;
        pos.y = 0;
        pos.z = 0;
        spineTrm.rotation = Quaternion.Euler(pos);
    }

    void Move()
    {
        if (Input.GetButtonDown("Jump") && (cf & CollisionFlags.Below) != 0)
        {
            //StartCoroutine(JumpCoroutine());
            animator.SetTrigger(hashJump);
            //yVelocity = jumpPowr;
            StartCoroutine(JumpCoroutine());
        }

        if (isJump)
        {
            yVelocity = 9.81f * Time.deltaTime * playerData.jumpPower;
        }
        else
        {
            yVelocity = -9.81f * Time.deltaTime * playerData.jumpPower;
        }


        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        animator.SetFloat(hashH, h);
        animator.SetFloat(hashV, v);

        Vector3 dir = Vector3.right * h + Vector3.forward * v;
        dir = MainCam.transform.TransformDirection(dir);
        dir.Normalize();
        dir.y = yVelocity;

        if(dir != Vector3.zero)
        {
            transform.rotation = Quaternion.Euler(0, MainCam.transform.eulerAngles.y, 0);
        }
        cf = cc.Move(dir * playerData.moveSpeed * Time.deltaTime);
    }

    private IEnumerator JumpCoroutine()
    {
        isJump = true;
        yield return new WaitForSeconds(.5f);
        isJump = false;
    }

    public void Damage(int amount)
    {
        currentHp -= amount;
        UIManager.Instance.PlayerHpbarValue(GetComponent<IHpController>());

        if (currentHp <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Die");
    }

    public void Knockback(float knockbackPower, Vector3 direction)
    {
        Vector3 dir = direction * knockbackPower;
        transform.position += dir;
    }
}
