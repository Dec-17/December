using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float ArrowSpeed = 15f; //ȭ���� ���ư��� �ӵ�

    void Start()
    {
        Destroy(gameObject, 2f); //x�� �Ŀ� ȭ�� ������Ʈ �ı�
    }

    void Update()
    {
        transform.Translate(Vector3.down * ArrowSpeed * Time.deltaTime); //ArrowSpeed �ӵ��� �̵�
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