using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeMonster : Mob
{
    public override void Test12()
    {
        base.Test12();
        Debug.Log("�ڽ��Դϴ�.");
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
