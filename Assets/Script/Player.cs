using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
class KeyNItem
{
    public KeyCode Key;
    public DemandItem Item;
}

public class Player : MonoBehaviour
{

    [SerializeField]
    List<KeyNItem> KeySettingList = new List<KeyNItem>();

    public float floatHeight = 2.0f;

    GameManager GM;

    // Start is called before the first frame update
    void Start()
    {
        GM = GameManager.inst;
    }

    // Update is called once per frame
    void Update()
    {
        if (!(GM.isGameOver || GM.isGamePause))
        {
            float groundYPos = GameManager.inst.ground.transform.position.y;
            transform.position = new Vector3(transform.position.x, groundYPos + floatHeight, transform.position.z);

            foreach (KeyNItem kni in KeySettingList)
            {
                if (Input.GetKeyDown(kni.Key))
                {
                    GameManager.inst.monsterManager.Compare(kni.Item);
                    Debug.Log("" + kni.Key + " " + kni.Item);
                }
            }
        }
    }
}
