using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using UnityEngine;
using Random = UnityEngine.Random;


public enum DemandItem
{
    PunpkinPie,
    Lollypop,
    Chocolate,
    Cookie
}

[Serializable]
public struct ItemNObj
{
    public DemandItem item;
    public GameObject obj;
}

public class Monster : MonoBehaviour
{
    Array demandItemTypes = Enum.GetValues(typeof(DemandItem));

    [SerializeField]
    List<GameObject> itemPrefabs = new List<GameObject>();

    [SerializeField]
    List<ItemNObj> ItemNObjs = new List<ItemNObj>();
    [Header("Item")]
    public int itemNum = 1;
    int currentItemIndex = 0;
    [Header("Animation")]
    public float dampingAmount = 0.85f;
    public Vector2 StopPos;

    public bool isFirst = false;

    GameObject tipObj;
    Vector3 itemGap;

    GameManager GM;

    // Start is called before the first frame update
    void Start()
    {
        GM = GameManager.inst;

        tipObj = transform.Find("Tip").gameObject;
        Debug.Log(tipObj);
        itemGap = tipObj.transform.localPosition;
        tipObj.transform.localPosition = itemGap + (Vector3.up * tipObj.transform.localScale.y * (itemNum - 1) / 2f);
        tipObj.transform.localScale = new Vector3(tipObj.transform.localScale.x, tipObj.transform.localScale.y * itemNum, tipObj.transform.localScale.z);   

        for (int i = itemNum-1; i >= 0; i--)
        {
            ItemNObj temp;
            temp.item = (DemandItem)demandItemTypes.GetValue(Random.Range(0, demandItemTypes.Length));
            Vector3 dPos = itemGap + (Vector3.up * i * tipObj.transform.localScale.x);
            temp.obj = Instantiate(itemPrefabs[(int)temp.item], transform);
            temp.obj.transform.localPosition = dPos;
            temp.obj.transform.localScale = Vector3.one * (tipObj.transform.localScale.x - 0.1f);
            ItemNObjs.Add(temp);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!(GM.isGameOver || GM.isGamePause))
        {
            transform.position = Vector3.Lerp(StopPos, transform.position, dampingAmount);

            //    if (isFirst)
            //    {
            //
            //    }
        }
    }

    public bool Compare(DemandItem dItem)
    {
        ItemNObj currentItem = ItemNObjs[currentItemIndex];
        if (currentItem.item == dItem)
        {
            currentItem.obj.GetComponent<SpriteRenderer>().enabled = false;
            ItemNObjs[currentItemIndex] = currentItem;
            currentItemIndex++;
            if (currentItemIndex >= itemNum)
            {
                GM.levelTimer += GM.regenTime;
                GM.passDessertNum += itemNum;
                GM.passCustomer++;
                GM.combo++;
                return true;
            }
        }
        else
        {
            GM.levelTimer -= GM.panaltyTime;
            GM.passDessertNum -= itemNum;
            GM.combo = 0;
            foreach (ItemNObj item in ItemNObjs)
            {
                item.obj.GetComponent<SpriteRenderer>().enabled = true;
                currentItemIndex = 0;
            }
        }

        return false;
    }
}
