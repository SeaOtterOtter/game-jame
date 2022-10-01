using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum ClassType
{
    Human,
    Zombie,
    Ghost,
    Pumpkin,
}


public enum DemandItem
{
    Candy,
    Apple,
    Hand,
    Bone
}

public class Monster : MonoBehaviour
{
    [SerializeField]
    List<GameObject> itemPrefabs = new List<GameObject>();

    Array classTypes = Enum.GetValues(typeof(ClassType));
    Array demandItes = Enum.GetValues(typeof(DemandItem));

    GameObject tipObj;
    SpriteRenderer tipObjRenderer;
    SpriteRenderer tipObjChildRenderer;

    public ClassType classType;
    public DemandItem demandItem;

    public float moveSpeed;
    public Vector2 StopPos;

    public bool isFirst = false;

    // Start is called before the first frame update
    void Start()
    {
        tipObj = transform.Find("Tip").gameObject;
        tipObjRenderer = tipObj.GetComponent<SpriteRenderer>();

        classType = (ClassType)classTypes.GetValue(Random.Range(0, classTypes.Length));
        demandItem = (DemandItem)demandItes.GetValue(Random.Range(0, demandItes.Length));

        tipObjChildRenderer = Instantiate(itemPrefabs[(int)demandItem], tipObj.transform).GetComponent<SpriteRenderer>();

        tipObjRenderer.enabled = true;
        tipObjChildRenderer.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(StopPos, transform.position, 0.85f);
        //if (transform.position.x >= StopPos.x)
        //{
        //    transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        //    tipObjRenderer.enabled = false;
        //    tipObjChildRenderer.enabled = false;
        //}
        //else
        //{
            
        //    if (isFirst)
        //    {
        //        tipObjRenderer.enabled = true;
        //        tipObjChildRenderer.enabled = true;
        //        //ShowTip();
        //    }
        //}
    }

    void ShowTip()
    {
        // TODO: �ִϸ��̼� Ʈ����
        tipObjRenderer.enabled = true;
    }
}
