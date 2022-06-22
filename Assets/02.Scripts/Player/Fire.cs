using System;
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
    private int bulletCnt;
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
    private float gunChangeTimer = 0f;
    [SerializeField]
    private float gunChangeDelay;

    private bool isAutoShoot => Input.GetMouseButton(0);
    private bool isReloading = false;

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
        UIManager.Instance.ChangeGunInfo(currentGunData.GunData.gunName, currentGunData.GunData.gunImage);

        timer = currentGunData.GunData.delay;
        bulletCnt = currentGunData.GunData.bulletCount;
        UIManager.Instance.SetBulletCountAndImage(bulletCnt, currentGunData.GunData.bulletPrefab.GetComponent<Bullet>().BulletSO.bulletImage);
    }

    void Update()
    {
        gunChangeTimer += Time.deltaTime;

        if (isReloading == false)
        {
            float wheelInput = Input.GetAxis("Mouse ScrollWheel");
            if (gunChangeTimer >= gunChangeDelay)
            {
                if (wheelInput > 0)
                {
                    GunDataCnt = (GunDataCnt + 1) % gunList.Count;
                    gunChangeTimer = 0f;
                }
                else if (wheelInput < 0)
                {
                    GunDataCnt = Mathf.Abs(GunDataCnt - 1) % gunList.Count;
                    gunChangeTimer = 0f;
                }
            }
        }

        timer += Time.deltaTime;

        animator.SetBool(hashAutoShoot, isAutoShoot && !isReloading);
        if(timer >= currentGunData.GunData.delay && isReloading == false)
        {
            if (bulletCnt > 0)
            {
                if (isAutoShoot)
                {
                    bulletCnt--;
                    UIManager.Instance.SetBulletCountAndImage(bulletCnt);
                    Bullet bullet = PoolManager.Instance.Pop("Bullet") as Bullet;
                    bullet.SetDamage(currentGunData.GunData.damage);
                    bullet.GetComponent<TrailRenderer>().Clear();
                    bullet.transform.SetPositionAndRotation(firePos.position, firePos.rotation);

                    MuzzleImpact muzzle = PoolManager.Instance.Pop("MuzzleFlash") as MuzzleImpact;
                    muzzle.transform.SetPositionAndRotation(firePos.position, firePos.rotation);
                    timer = 0f;
                }
            }
            else
            {
                StartCoroutine(Reload());
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        UIManager.Instance.SetReloadImageActive(true);
        yield return new WaitForSeconds(currentGunData.GunData.reloadDelay); 
        bulletCnt = currentGunData.GunData.bulletCount;
        UIManager.Instance.SetBulletCountAndImage(bulletCnt);
        UIManager.Instance.SetReloadImageActive(false);
        isReloading = false;
    }

    void GetGun()
    {
        AllClear();
        gunList[GunDataCnt].SetActive(true);

        currentGun = gunList[GunDataCnt];
        currentGunData = currentGun.GetComponent<Gun>();
        UIManager.Instance.ChangeGunInfo(currentGunData.GunData.gunName, currentGunData.GunData.gunImage);
        firePos = gunList[GunDataCnt].transform.Find("FirePos");
        animator.runtimeAnimatorController = currentGunData.GunData.animator;
        bulletCnt = currentGunData.GunData.bulletCount;
        UIManager.Instance.SetBulletCountAndImage(bulletCnt, currentGunData.GunData.bulletPrefab.GetComponent<Bullet>().BulletSO.bulletImage);
    }

    void AllClear()
    {
        foreach (GameObject obj in gunList)
        {
            obj.SetActive(false);
        }
    }
}
