using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    private static Camera mainCam = null;
    private static Transform playerTrm = null;
    private static Transform nexusTrm = null;
    private static Transform enemySpawnPos = null;
    private static GameObject spawnDoor = null;

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

    public static Transform PlayerTrm
    {
        get
        {
            if(playerTrm == null)
            {
                playerTrm = GameObject.FindWithTag("Player").transform;
            }
            return playerTrm;
        }
    }

    public static Transform NexusTrm
    {
        get
        {
            if(nexusTrm == null)
            {
                nexusTrm = GameObject.FindWithTag("Nexus").transform.GetChild(0).transform;
            }
            return nexusTrm;
        }
    }

    public static Transform EnemySpawnPos
    {
        get
        {
            if(enemySpawnPos == null)
            {
                enemySpawnPos = GameObject.FindWithTag("EnemySpawn").transform;
            }
            return enemySpawnPos;
        }
    }

    public static GameObject SpawnDoor
    {
        get
        {
            if(spawnDoor == null)
            {
                spawnDoor = GameObject.FindWithTag("Door");
            }
            return spawnDoor;
        }
    }
}
