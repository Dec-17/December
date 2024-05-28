using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStart : MonoBehaviour
{
    public GameObject bossObj;
    public float bossLength = 10f;
    public GameObject wall;

    void Start()
    {
        
    }

    void Update()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");  //Player 태그를 가진 오브젝트 찾기

        foreach (GameObject player in players)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position); //플레이어와 현재 스크립트를 가진 오브젝트 사이의 거리측정

            if (distance <= bossLength) //거리가 bossLength 이내에 있다면
            {
                Debug.Log("보스전 시작");
                bossObj.SetActive(true);
                wall.SetActive(true);
            }
        }
    }
}
