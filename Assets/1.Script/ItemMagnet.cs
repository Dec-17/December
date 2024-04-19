using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMagnet : MonoBehaviour
{
    public float followSpeed = 10f; //플레이어를 따라가는 속도
    public float detectionRange = 5f; //아이템이 플레이어를 감지할 범위

    void Update()
    {
        PlayerController player = FindObjectOfType<PlayerController>(); // 씬에서 플레이어 찾기

        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);

            if (distance <= detectionRange) // 플레이어가 감지 범위 내에 있을 때 이동
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, followSpeed * Time.deltaTime);
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other) //아이템 획득 및 파괴
    {
        if (other.gameObject.CompareTag("Player")) //Player와 충돌이 일어난다면
        {
            //아이템 획득 후
            Destroy(gameObject); //아이템을 획득했으므로 해당 오브젝트 파괴
        }
    }
}
