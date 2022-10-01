using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum eGiftCategory
{
    Key1,
    Key2,
    Key3,
    Key4,
    Key5,
    Key6,
    Key7,
    Key8,
    KeyLength,
}




public class GiftBox : MonoBehaviour
{
    //LinkedList<Tuple<Gift, bool>> listGiftTuple;

    [SerializeField]
    float elementDiff;
    //eGiftCategory giftCategory;
    //bool activeFlag;
    LinkedList<Gift> listGiftClone;


    public GiftBox()
    {
        //listGiftTuple = new LinkedList<Tuple<Gift, bool>>();

        /*

                for (int i =0; i < gift.Count; ++i)
                {
                    listGiftTuple.AddLast(
                        new Tuple<Gift , bool>(gift.ElementAt(i), false)
                    );
                }*/
        listGiftClone = new LinkedList<Gift>();
        
    }

    public void Update()
    {
        
    }
    //public eGiftCategory GiftCategory { get; set; }
    //set 할 때 색깔 바꾸기
    //public bool ActiveFlag { get; set; }
    public void CheckGiftIsValid(Gift gift)
    {
       

        for (int i = 0; i < listGiftClone.Count; ++i)
        {
            if (listGiftClone.ElementAt(i).gameObject.activeSelf)
            {
                if (listGiftClone.ElementAt(i).GiftCategory == gift.GiftCategory)
                {
                    //listGiftClone.ElementAt(i).ActiveFlag = false;
                    listGiftClone.ElementAt(i).gameObject.SetActive(false);


                    if (CheckAllButtonDown())
                    {

                        GameManager.instance.FirstCustomerIsDone();

                        


                    }

                    return;
                }
            }

            

           


        }





        //여기 넘어왔다는 거는 없다는 거임 , 패널티 주고 초기화
        GameManager.instance.ApplyPenaltyScore();
        
        ResetGiftBox();

    }

    public void AddGift(Gift gift)
    {

        listGiftClone.AddLast(Instantiate(gift, new Vector3(0, 0, 0), Quaternion.identity));
        //listGiftClone.AddLast(gift);
        listGiftClone.Last.Value.gameObject.SetActive(true);

        listGiftClone.Last.Value.transform.SetParent(this.gameObject.transform);


        UpdateGiftBoxLayout();
        //giftCloneArray[giftCloneArray.Length].gameObject.SetActive(true);

    }

    


    public void UpdateGiftBoxLayout()
    {
        for(int i = 0; i < listGiftClone.Count; ++i)
        {
            if (i == 0)
            {
                //listGiftTuple.First.Value
                listGiftClone.ElementAt(i).transform.position = new Vector2(this.transform.position.x , this.transform.position.y - elementDiff);

            }
            else if(i == 1)
            {
                listGiftClone.ElementAt(i).transform.position = new Vector2(this.transform.position.x, this.transform.position.y);
            }
            else if(i== 2)
            {
                listGiftClone.ElementAt(i).transform.position = new Vector2(this.transform.position.x, this.transform.position.y + elementDiff);
            }
        }
    }

    private void ResetGiftBox()
    {
        for(int i = 0; i < listGiftClone.Count; ++i)
        {
            listGiftClone.ElementAt(i).gameObject.SetActive(true);

        }
    }

    private bool CheckAllButtonDown()
    {

        for(int i = 0; i < listGiftClone.Count; ++i)
        {
            if(listGiftClone.ElementAt(i).gameObject.activeSelf == true)
            {
                return false;
            }
        }

        return true;
    }
}
