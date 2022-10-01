using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Dictionary<KeyCode, Gift> mapKeyCodeToGift;
    public Dictionary<KeyCode, Gift> MapKeyCodeToGift{ get { return mapKeyCodeToGift; } }


    [SerializeField]
    Gift[] giftArray;

    // Start is called before the first frame update
    void Start()
    {
        mapKeyCodeToGift = new Dictionary<KeyCode, Gift>();
        InitMapKeyCodeToGift();
    }

    // Update is called once per frame
    void Update()
    {
        InputKey();
    }

    void InitMapKeyCodeToGift()
    {
        mapKeyCodeToGift.Add(KeyCode.Q, giftArray[0]);
        mapKeyCodeToGift.Add(KeyCode.W, giftArray[1]);
        mapKeyCodeToGift.Add(KeyCode.E, giftArray[2]);
        //mapKeyCodeToGift.Add(KeyCode.R, giftArray[0]);
        //mapKeyCodeToGift.Add(KeyCode.RightArrow, giftArray[0]);
        //mapKeyCodeToGift.Add(KeyCode.LeftArrow, giftArray[0]);
        //mapKeyCodeToGift.Add(KeyCode.UpArrow, giftArray[0]);
        //mapKeyCodeToGift.Add(KeyCode.DownArrow, giftArray[0]);

    }

    void InputKey()
    {

        Customer firstCustomer = GameManager.instance.GetFirstCustomer();
        Gift tempGift = null;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            tempGift = MapKeyCodeToGift[KeyCode.Q];

            //����� customer���� ó�� , ���⼭�� �ѱ�⸸
            firstCustomer.PassGift(tempGift);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            tempGift = MapKeyCodeToGift[KeyCode.W];

            //����� customer���� ó�� , ���⼭�� �ѱ�⸸
            firstCustomer.PassGift(tempGift);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            tempGift = MapKeyCodeToGift[KeyCode.E];

            //����� customer���� ó�� , ���⼭�� �ѱ�⸸
            firstCustomer.PassGift(tempGift);
        }



    }


}
