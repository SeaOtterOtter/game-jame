using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager inst;

    public MonsterManager monsterManager;

    public GameObject player;

    public GameObject ground;

    float Timer = 0.0f;

    void Awake()
    {
        inst = this;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}
