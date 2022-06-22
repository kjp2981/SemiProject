using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;


public class EnemySpawner : MonoBehaviour
{
    #region Singleton
    private static EnemySpawner instance = null;
    public static EnemySpawner Instance
    {
        get
        {
            if (instance == null)
            {
                instance = null;
            }
            return instance;
        }
    }
    #endregion

    [SerializeField]
    private MonsterSpawnSO[] spawnData;

    private int stageCnt = 1;

    public int StageCnt
    {
        get => spawnData.Length;
    }

    public Dictionary<int, List<MonsterPair>> spawnDictionary = new Dictionary<int, List<MonsterPair>>();

    private int monsterCnt = 0;
    private int deadCnt = 0;

    private Animator spawnDoorAnimator;
    private readonly int hashIsOpen = Animator.StringToHash("isOpen");

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        spawnDoorAnimator = SpawnDoor.GetComponent<Animator>();

        for (int i = 0; i < spawnData.Length; i++)
        {
            spawnDictionary.Add(i + 1, spawnData[i].monsterSpawnList);
        }
    }

    public bool isStage()
    {
        return deadCnt != monsterCnt;
    }

    public IEnumerator SpawnEnemy(int stage)
    {
        //for (int idx = 1; idx <= spawnData.Length; idx++)
        //{
        //    foreach (KeyValuePair<int, List<MonsterPair>> pair in spawnDictionary)
        //    {
        //        for (int i = 0; i < pair.Value.Count; i++)
        //        {
        //            monsterCnt += pair.Value[i].count;
        //            yield return new WaitForSeconds(2f);
        //            for (int j = 0; j < pair.Value[i].count; j++)
        //            {
        //                yield return new WaitForSeconds(1);
        //                Monster monster = PoolManager.Instance.Pop(pair.Value[i].prefab.name) as Monster;
        //                //monster.transform.position = EnemySpawnPos.position;
        //                monster.transform.position = EnemySpawnPos.localPosition;
        //            }
        //        }
        //    }
        //    yield return new WaitForSeconds(10);
        //}

        monsterCnt = 0;
        deadCnt = 0;
        foreach (KeyValuePair<int, List<MonsterPair>> pair in spawnDictionary)
        {
            if (pair.Key == stage)
            {
                spawnDoorAnimator.SetBool(hashIsOpen, true);
                for (int i = 0; i < pair.Value.Count; i++)
                {
                    monsterCnt += pair.Value[i].count;
                }
                for (int i = 0; i < pair.Value.Count; i++)
                {
                    yield return new WaitForSeconds(2f);
                    for (int j = 0; j < pair.Value[i].count; j++)
                    {
                        yield return new WaitForSeconds(1);
                        Monster monster = PoolManager.Instance.Pop(pair.Value[i].prefab.name) as Monster;
                        monster.transform.position = SpawnDoor.transform.localPosition;
                    }
                }
                spawnDoorAnimator.SetBool(hashIsOpen, false);
            }
        }
    }

    public void DeadCount()
    {
        deadCnt += 1;
    }
}