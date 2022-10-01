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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        foreach(KeyNItem kni in KeySettingList)
        {
            if(Input.GetKeyDown(kni.Key))
            {
                GameManager.inst.monsterManager.Compare(kni.Item);
                Debug.Log("" + kni.Key + " " + kni.Item);
            }
        }
    }
}
