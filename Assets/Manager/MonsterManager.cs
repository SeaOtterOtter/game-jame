using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

public class MonsterManager : MonoBehaviour
{
    [SerializeField]
    List<GameObject> monsterPrefabs = new List<GameObject>();
    [SerializeField]
    List<GameObject> monsterList = new List<GameObject>();
    [Space(10)]
    public int maxNum = 10;
    [Space(10)]
    public float gapDistance = 1.0f;
    public int spawnDistance = 12;
    [Space(10)]
    public float spawnDelay = 1.0f;
    
    float spawnTimer = 0.0f;

    Vector3 playerPos;

    GameManager GM;

    // Start is called before the first frame update
    void Start()
    {
        GM = GameManager.inst;
        playerPos = GM.player.transform.position;

        spawnTimer = 0.0f;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!(GM.isGameOver || GM.isGamePause))
        {
            spawnTimer += Time.deltaTime;

            if (spawnTimer >= spawnDelay && monsterList.Count <= maxNum)
            {
                SpawnMonster();
                spawnTimer = 0.0f;
            }
        }
    }

    void SpawnMonster()
    {
        Vector3 spawnPoint = new Vector3(playerPos.x + gapDistance * (spawnDistance), playerPos.y, 0);
        GameObject tempMonObj = monsterPrefabs[Random.Range(0, monsterPrefabs.Count)];
        GameObject p = Instantiate(tempMonObj, spawnPoint, Quaternion.Euler(0, 180f, 0));
        p.transform.parent = this.transform;

        Monster m = p.GetComponent<Monster>();
        playerPos = GM.player.transform.position; // 플레이어 참고 위치 갱신 Note: Release 때 빼기
        m.itemNum = Random.Range(GM.minDessertNum, GM.maxDessertNum+1);
        m.StopPos = new Vector3(playerPos.x + gapDistance * (monsterList.Count + 1), playerPos.y, 0);
        if (monsterList.Count == 0)
            m.isFirst = true;

        monsterList.Add(p);
        Debug.Log(monsterList.Count);

        GM.spawnCustomer++;
    }

    public void Compare(DemandItem dItem)
    {
        if (monsterList.Count != 0)
        {
            GameObject monster;
            monster = monsterList[0];

            // 여기서 맞는지 판정
            if (monster.GetComponent<Monster>().Compare(dItem))
            {
                Debug.Log("OK!");

                monsterList.Remove(monster);
                Destroy(monster);

                // 모든 몬스터 목표 좌표 재계산
                if (monsterList.Count != 0)
                {
                    monsterList[0].GetComponent<Monster>().isFirst = true;
                    for (int i = 0; i < monsterList.Count; i++)
                    {
                        Vector3 stoppos = new Vector3(playerPos.x + gapDistance * (i + 1), playerPos.y, 0);
                        monsterList[i].GetComponent<Monster>().StopPos = stoppos;
                        // monsterList[i].transform.position = stoppos;
                    }
                }
            }
        }
        Debug.Log(monsterList.Count);
    }
}
