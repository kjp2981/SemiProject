using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Fire : MonoBehaviour
{
    [SerializeField]
    private float delay = 0.5f;

    private float timer = 0f;

    private bool isAutoShoot => Input.GetMouseButton(0);

    public Transform firePos;

    private Animator animator;

    private readonly int hashShoot = Animator.StringToHash("shoot");
    private readonly int hashAutoShoot = Animator.StringToHash("autoShoot");

    void Start()
    {
        animator = GetComponent<Animator>();

        timer = delay;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= delay)
        {
            animator.SetBool(hashAutoShoot, isAutoShoot);
            if (Input.GetMouseButtonDown(0))
            {
                // TODO : Fire!
                StartCoroutine(Shoot());

                timer = 0f;
            }
            else if (isAutoShoot)
            {
                Bullet bullet = PoolManager.Instance.Pop("Bullet") as Bullet;
                bullet.transform.SetPositionAndRotation(firePos.position, firePos.rotation);
                timer = 0f;
            }
        }
    }

    IEnumerator Shoot()
    {
        animator.SetTrigger(hashShoot);
        yield return new WaitForSeconds(.5f);
        Bullet bullet = PoolManager.Instance.Pop("Bullet") as Bullet;
        bullet.transform.SetPositionAndRotation(firePos.position, firePos.rotation);
    }
}
