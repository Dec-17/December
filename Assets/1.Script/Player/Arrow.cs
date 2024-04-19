using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float ArrowSpeed = 15f; //화살이 날아가는 속도

    void Start()
    {
        Destroy(gameObject, 2f); //x초 후에 화살 오브젝트 파괴
    }

    void Update()
    {
        transform.Translate(Vector3.down * ArrowSpeed * Time.deltaTime); //ArrowSpeed 속도로 이동
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Item"))
        {
            {
                return;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}