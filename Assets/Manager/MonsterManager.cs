using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

public class MonsterManager : MonoBehaviour
{
    public GameObject monsterPrefab;
    [Space(10)]
    public Vector2 monSpawnPoint;
    public float monSpawnDelay = 1.0f;
    public float monMoveSpeed = 2.0f;
    [Space(10)]
    float monSpawnTimer = 0.0f;

    [SerializeField]
    List<GameObject> monsterList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        monSpawnTimer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        monSpawnTimer += Time.deltaTime;

        if (monSpawnTimer >= monSpawnDelay)
        {
            SpawnMonster();
            monSpawnTimer = 0.0f;
        }
    }

    public void Compare()
    {
        if (monsterList.Count != 0)
        {
            GameObject monster;
            monster = monsterList[0];
            monsterList.Remove(monster);
            Destroy(monster);
            if (monsterList.Count != 0)
            {
                foreach(GameObject m in monsterList)
                {
                    m.GetComponent<Monster>().isMoving = true;
                }
            }
        }
        Debug.Log(monsterList.Count);
    }

    void SpawnMonster()
    {
        GameObject p = Instantiate(monsterPrefab, monSpawnPoint, Quaternion.Euler(0, 180f, 0));
        p.transform.parent = this.transform;

        p.GetComponent<Monster>().moveSpeed = monMoveSpeed;

        monsterList.Add(p);
        Debug.Log(monsterList.Count);
    }
}
