using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    LinkedList<Customer> customerList;


    [SerializeField]
    TextMeshProUGUI scoreText;


    int score;
    [SerializeField]
    int penaltyScore;
    public int PenaltyScore { get;  }

    [SerializeField]
    Gift[] giftPrefab;
    [SerializeField]
    Customer customerPrefab;

    [SerializeField]
    Vector2 firstCustomerPosition;
    [SerializeField]
    float customerDiffX;

    //public int Score { get; set; }
    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        
        
    }

    private void Start()
    {
        //Customer cust = new Customer();
        customerList = new LinkedList<Customer>();


        //customerPrefab.AddGift(giftPrefab[0]);
        //customerPrefab.AddGift(giftPrefab[1]);
        //customerPrefab.AddGift(giftPrefab[2]);


        for (int i = 0; i < 10; ++i)
        {


            customerList.AddLast(Instantiate(customerPrefab, new Vector2(firstCustomerPosition.x + customerDiffX * i, firstCustomerPosition.y), Quaternion.identity));



            customerList.Last.Value.AddGift(giftPrefab[0]);
            customerList.Last.Value.AddGift(giftPrefab[1]);
            customerList.Last.Value.AddGift(giftPrefab[2]);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("hi");
        }
    }

    public Customer GetFirstCustomer()
    {
        if(customerList.Count > 0)
        {
            return customerList.First.Value;
        }

        return null;
    }

    public void PlusScore(int score)
    {
        if (score > 0)
        {
            this.score += score;
        }

        scoreText.text = this.score.ToString();
    }

    public void ApplyPenaltyScore()
    {
        this.score -= penaltyScore;

        if (this.score < 0)
        {
            this.score = 0;
        }

        scoreText.text = this.score.ToString();
    }

    public void PulltoLeftCustomer()
    {
        //앞에 있는 커스터머 점수가져가기 

        Destroy(customerList.First.Value.gameObject);
        customerList.RemoveFirst();

        StopCoroutine("PullCustomer");
        StartCoroutine("PullCustomer");


    }
    IEnumerator PullCustomer()
    {
        //한번에 10명

        int Count = customerList.Count > 10 ? 10 : customerList.Count;


        while(true)
        {
            for (int i = 0; i < Count; ++i)
            {
                Transform trs = customerList.ElementAt(i).transform;


                customerList.ElementAt(i).transform.position = Vector2.Lerp(trs.position,
                    new Vector2(firstCustomerPosition.x + customerDiffX * i, firstCustomerPosition.y), 0.3f);


            }
            yield return new WaitForSeconds(0.01f);
        }
       

        
    }
    public void FirstCustomerIsDone()
    { 
        PlusScore(100);

        PulltoLeftCustomer();

    }


    /* Customer GetRandomCustomer()
     {
         //x -4.5 , -3.5

     }*/
}
