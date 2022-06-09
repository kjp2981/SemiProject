using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Fire : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> gunList;
    private GameObject currentGun;
    private Gun currentGunData;

    private int gunDataCnt = 0;
    public int GunDataCnt
    {
        get => gunDataCnt;
        private set
        {
            gunDataCnt = value;
            GetGun();
        }
    }

    private float timer = 0f;

    private bool isAutoShoot => Input.GetMouseButton(0);

    public Transform gunPos;
    private Transform firePos;

    private Animator animator;

    private readonly int hashAutoShoot = Animator.StringToHash("autoShoot");

    void Start()
    {
        animator = GetComponent<Animator>();
        firePos = gunPos.transform.GetChild(0).Find("FirePos");
        currentGun = gunList[0];
        currentGunData = currentGun.GetComponent<Gun>();

        timer = currentGunData.GunData.delay;
    }

    void Update()
    {
        float wheelInput = Input.GetAxis("Mouse ScrollWheel");
        if(wheelInput > 0)
        {
            GunDataCnt = (GunDataCnt + 1) % gunList.Count;
        }
        else if(wheelInput < 0)
        {
            GunDataCnt = Mathf.Abs(GunDataCnt - 1) % gunList.Count;
        }

        timer += Time.deltaTime;

        if(timer >= currentGunData.GunData.delay)
        {
            animator.SetBool(hashAutoShoot, isAutoShoot);
            if (isAutoShoot)
            {
                Bullet bullet = PoolManager.Instance.Pop("Bullet") as Bullet;
                bullet.transform.SetPositionAndRotation(firePos.position, firePos.rotation);

                MuzzleImpact muzzle = PoolManager.Instance.Pop("MuzzleFlash") as MuzzleImpact;
                muzzle.transform.SetPositionAndRotation(firePos.position, firePos.rotation);
                timer = 0f;
            }
        }
    }

    void GetGun()
    {
        AllClear();
        gunList[GunDataCnt].SetActive(true);

        currentGun = gunList[GunDataCnt];
        currentGunData = currentGun.GetComponent<Gun>();
        firePos = gunList[GunDataCnt].transform.Find("FirePos");
        animator.runtimeAnimatorController = currentGunData.GunData.animator;
    }

    void AllClear()
    {
        foreach (GameObject obj in gunList)
        {
            obj.SetActive(false);
        }
    }
}
