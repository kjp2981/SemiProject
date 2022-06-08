using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

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

    public Transform firstCameraPos;
    #endregion

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

        rx += rotSpeed * my * Time.deltaTime;
        ry += rotSpeed * mx * Time.deltaTime;

        rx = Mathf.Clamp(rx, -80, 80);
        //ry = Mathf.Clamp(ry, -90, 90);

        transform.eulerAngles = new Vector3(-rx, ry, 0);
        transform.position = firstCameraPos.position;
    }
}
