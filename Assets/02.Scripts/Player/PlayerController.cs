using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private CharacterController cc;
    private CollisionFlags cf = CollisionFlags.None;

    [SerializeField]
    private float moveSpeed = 10f;
    [SerializeField]
    private float jumpPowr = 5f;
    private float yVelocity;

    private readonly int hashH = Animator.StringToHash("h");
    private readonly int hashV = Animator.StringToHash("v");
    private readonly int hashJump = Animator.StringToHash("jump");
    void Start()
    {
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        yVelocity += -9.81f * Time.deltaTime;
        if (Input.GetButtonDown("Jump") && (cf & CollisionFlags.Below) != 0)
        {
            animator.SetTrigger(hashJump);
            yVelocity = jumpPowr;
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
        cf = cc.Move(dir * moveSpeed * Time.deltaTime);
    }
}