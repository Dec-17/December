using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject talkTrue; //대화 가능 범위에 들어왔는지 표시
    public bool isInRange = false; //대화 가능 범위에 들어왔는지 확인하는 값
    public float talkLength = 3; //대화 가능 범위

    void Start()
    {

    }

    void Update()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");  //Player 태그를 가진 오브젝트 찾기

        foreach (GameObject player in players)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position); //플레이어와 현재 스크립트를 가진 오브젝트 사이의 거리측정

            if (distance <= talkLength) //거리가 talkLength 이내에 있다면
            {
                isInRange = true; //대화 가능 범위에 들어옴
                break;
            }
            else
            {
                isInRange = false; //대화 가능 거리 밖임
            }
        }
        
        if (isInRange) //만약 범위 안에 있다면
        {
            Debug.Log("대화가능");
            //talkTrue 오브젝트의 알파값을 1로 설정
            Color color = talkTrue.GetComponent<Renderer>().material.color;
            color.a = 1f;
            talkTrue.GetComponent<Renderer>().material.color = color;
        }
        else
        {
            Debug.Log("대화불가능");
            //범위 밖에 있다면 talkTrue 오브젝트의 알파값을 0으로 설정
            Color color = talkTrue.GetComponent<Renderer>().material.color;
            color.a = 0f;
            talkTrue.GetComponent<Renderer>().material.color = color;
        }
    }
}
