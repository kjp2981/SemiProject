using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class CameraController : MonoBehaviour
{
    RaycastHit ray;
    [SerializeField]
    private float rayDistance = 1;
    private LayerMask cannonLayer;

    public enum CameraType
    {
        First,
        Three
    }
    public CameraType type = CameraType.First;

    #region 1인칭 카메라 제어 관련 변수
    float rx;
    float ry;

    [Header("1인칭 카메라")]
    [SerializeField]
    private float rotSpeed = 10f;

    [SerializeField]
    private Transform firstCameraPos;
    #endregion

    private IEnumerator Start()
    {
        //firstCameraPos = PlayerTrm.transform.Find("FirstCameraPos").GetComponent<Transform>();
        cannonLayer = 1 << LayerMask.NameToLayer("CannonSpawn");

        rotSpeed = 0;
        yield return new WaitForSeconds(.5f);
        rotSpeed = 10;
    }

    private void Update()
    {
        Physics.Raycast(transform.position, transform.forward, out ray, rayDistance, cannonLayer);
        Debug.DrawRay(transform.position, transform.forward * rayDistance);

        if(ray.collider != null)
        {
            UIManager.Instance.SetCannonTextActive(true);

            if (Input.GetKeyDown(KeyCode.F))
            {
                if(GoldManager.Instance.Gold >= 300)
                    CannonSpawner.Instance.SpawnCannon(ray.collider.transform);
            }
        }
        else
        {
            UIManager.Instance.SetCannonTextActive(false);
        }
    }

    void LateUpdate()
    {
        switch (type)
        {
            case CameraType.First:
                FirstRotate();
                break;
            case CameraType.Three:
                break;
        }
    }

    void FirstRotate()
    {
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");

        rx += rotSpeed * my * Time.deltaTime * rotSpeed;
        ry += rotSpeed * mx * Time.deltaTime * rotSpeed;

        rx = Mathf.Clamp(rx, -30, 60);
        //ry = Mathf.Clamp(ry, -90, 90);

        transform.eulerAngles = new Vector3(-rx, ry, 0);
        transform.position = firstCameraPos.position;
    }
}
