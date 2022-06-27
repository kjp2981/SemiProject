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
    private float zOffset = -13.881f;

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


    }

    //void LateUpdate()
    //{
    //    Quaternion rot = MainCam.transform.rotation;
    //    rot.y = 0;
    //    rot.x = 0;
    //    rot.z *= -1;
    //    //rot.z += zOffset;
    //    spineTrm.localRotation = rot;

    //    //spineTrm.transform.LookAt(MainCam.transform.forward);
    //}

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

    //private void OnAnimatorIK(int layerIndex)
    //{
    //    animator.SetIKPosition(AvatarIKGoal.RightHand, crossHair.transform.position);
    //    animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0.5f);

    //    animator.SetIKPosition(AvatarIKGoal.LeftHand, crossHair.transform.position);
    //    animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0.5f);

    //    animator.SetIKRotation(AvatarIKGoal.RightHand, crossHair.transform.rotation);
    //    animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0.5f);

    //    animator.SetIKRotation(AvatarIKGoal.LeftHand, crossHair.transform.rotation);
    //    animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0.5f);
    //}
}
