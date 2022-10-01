using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.EventSystems;
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
    GameObject tipObj;

    Array ItemValues = Enum.GetValues(typeof(DemandItem));

    public DemandItem demandItem;
    public float moveSpeed;

    [SerializeField]
    public bool isMoving = true;

    // Start is called before the first frame update
    void Start()
    {
        isMoving = true;
        tipObj = transform.Find("Tip").gameObject;
        demandItem = (DemandItem)ItemValues.GetValue(Random.Range(0, ItemValues.Length));
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
    }

    void OnDestroy()
    {
        Debug.Log("Destroy");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "MonBack" || collision.tag == "Player")
        {
            isMoving = false;
            if (collision.tag == "Player")
                tipObj.GetComponent<SpriteRenderer>().enabled = true;
            Debug.Log("" + isMoving + " " + collision.tag);
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "MonBack" || collision.tag == "Player")
        {
            isMoving = false;
            Debug.Log("" + isMoving + " " + collision.tag);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "MonFront")
        {
            isMoving = true;
            Debug.Log("" + isMoving + " " + collision.tag);
        }
    }
}
