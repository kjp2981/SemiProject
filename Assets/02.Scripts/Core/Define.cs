using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    private static Camera mainCam = null;
    public static Camera MainCam
    {
        get
        {
            if(mainCam == null)
            {
                mainCam = Camera.main;
            }
            return mainCam;
        }
    }
}
