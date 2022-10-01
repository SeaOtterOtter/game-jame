using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] KeyCode A;
    [SerializeField] KeyCode B;
    [SerializeField] KeyCode C;
    [SerializeField] KeyCode D;
    [Space(10)]
    [SerializeField] KeyCode E;
    [SerializeField] KeyCode F;
    [SerializeField] KeyCode G;
    [SerializeField] KeyCode H;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(A))
        {
            GameManager.inst.monsterManager.Compare();
        }
    }
}
