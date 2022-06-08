using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController cc;
    private CollisionFlags cf = CollisionFlags.None;

    [SerializeField]
    private float moveSpeed = 10f;
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = Vector3.right * h + Vector3.forward * v;
        dir = MainCam.transform.TransformDirection(dir);
        dir += Physics.gravity;
        dir.Normalize();

        if(dir != Vector3.zero)
        {
            transform.rotation = Quaternion.Euler(0, MainCam.transform.eulerAngles.y, 0);
        }
        cf = cc.Move(dir * moveSpeed * Time.deltaTime);
    }
}
