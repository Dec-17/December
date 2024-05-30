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

    void OnTriggerEnter2D(Collider2D other) //화살 충돌
    {
        if (other.CompareTag("Player") || other.CompareTag("Item") || other.CompareTag("TileMap") || other.CompareTag("MainCamera"))//플레이어, 아이템, 타일맵은 통과
        {
            {
                return;
            }
        }
        else //그외의 오브젝트와 충돌 시 화살 파괴
        {
            //화살 부딫히는 소리
            Destroy(gameObject);
        }
    }
}