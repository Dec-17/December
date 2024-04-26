using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeMonster : Mob
{
    public override void Test12()
    {
        base.Test12();
        Debug.Log("자식입니다.");
    }


    void Start()
    {
        Test12();
    }

    

    void Update()
    {
        
    }
   

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
    }
}
