using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gift : MonoBehaviour
{

    public Gift(eGiftCategory giftCategory)
    {
        this.giftCategory = giftCategory;
        ActiveFlag = true;
    }

    [SerializeField]
    eGiftCategory giftCategory;
    public eGiftCategory GiftCategory { get { return giftCategory; } set { giftCategory = value; } }

    //bool activeFlag;
    public bool ActiveFlag { get; set; }

    [SerializeField]
    int score;
    public int Score { get { return score; } }
}