using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Customer : MonoBehaviour
{
    //gift flagBox;
    //LinkedList<GiftBox> giftList;
    [SerializeField]
    GiftBox giftBox;
    // Start is called before the first frame update
    void Start()
    {
       // giftBox = new GiftBox();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddGift(Gift gift)
    {
        giftBox.AddGift(gift);
    }
    
   /* private bool isHaveGift(Gift gift)
    {
        if(giftBox.Contains(gift))
        {
            return true;
        }
        
        return false;
    }*/

    public void PassGift(Gift gift)
    {
        //gift check, 여기서 점수알아서 해결함
        giftBox.CheckGiftIsValid(gift);
    }

    
}
