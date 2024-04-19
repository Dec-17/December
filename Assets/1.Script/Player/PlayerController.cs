using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerController : MonoBehaviour
{
    public float Speed = 5f; //플레이어 이동 속도
    Animator animator;
    SpriteRenderer spriteRenderer;
    public float moveHorizontal;
    public float moveVertical;
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        //플레이어 이동
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(moveHorizontal, moveVertical, 0f).normalized;
        transform.Translate(moveDirection * Speed * Time.deltaTime);

        
       
    }   
}