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
    List<GameObject> monsterList = new List<GameObject>();

    public GameObject monsterPrefab;

    [Space(10)]
    public int maxMonNum = 10;
    public Vector2 monSpawnPoint;
    public float monGapDistance = 1.0f;
    public float monSpawnDelay = 1.0f;
    public float monMoveSpeed = 2.0f;
    [Space(10)]
    float monSpawnTimer = 0.0f;

    Vector3 playerPos;

    // Start is called before the first frame update
    void Start()
    {
        monSpawnTimer = 0.0f;
        playerPos = GameManager.inst.player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        monSpawnTimer += Time.deltaTime;

        if (monSpawnTimer >= monSpawnDelay && monsterList.Count <= maxMonNum)
        {
            SpawnMonster();
            monSpawnTimer = 0.0f;
        }
    }

    void SpawnMonster()
    {
        GameObject p = Instantiate(monsterPrefab, monSpawnPoint, Quaternion.Euler(0, 180f, 0));
        p.transform.parent = this.transform;

        Monster m = p.GetComponent<Monster>();
        m.moveSpeed = monMoveSpeed;
        playerPos = GameManager.inst.player.transform.position; // 플레이어 참고 위치 갱신 Note: Release 때 빼기 
        m.StopPos = new Vector3(playerPos.x + monGapDistance * (monsterList.Count + 1), playerPos.y, 0);
        if (monsterList.Count == 0)
            m.isFirst = true;

        monsterList.Add(p);
        Debug.Log(monsterList.Count);
    }

    public void Compare(DemandItem dItem)
    {
        if (monsterList.Count != 0)
        {
            GameObject monster;
            monster = monsterList[0];

            // 여기서 맞는지 판정 // TODO: 아이템 여러개 판단
            if (monster.GetComponent<Monster>().demandItem == dItem)
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
                        Vector3 stoppos = new Vector3(playerPos.x + monGapDistance * (i + 1), playerPos.y, 0);
                        monsterList[i].GetComponent<Monster>().StopPos = stoppos;
                        // monsterList[i].transform.position = stoppos;
                    }
                }
            }
        }
        Debug.Log(monsterList.Count);
    }
}
