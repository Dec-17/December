using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;
using UnityEditor.Rendering;

public class TextManager : MonoBehaviour
{
    private float talkLength = 3f; //대화 가능 범위
    public GameObject talkTrue; //대화 가능한 범위에 들어왔는지 표시
    private bool isInRange = false; //대화 가능 범위인지 확인

    public Text dialogueText; //다이얼로그 텍스트
    public GameObject dialogueWindow; //다이얼로그 대화칸

    private float typingSpeed = 0.1f; //타이핑모션 속도
    private bool isTyping = false; // 타이핑 모션 실행 여부 확인

    public string[] talk; //다이얼로그 텍스트를 입력 할 배열
    private int currentTalkIndex = 0; //현재 출력 중인 talk의 인덱스
    
    void Start()
    {
        //게임 실행 시 대화창 UI 비활성화
        dialogueText.gameObject.SetActive(false);
        dialogueWindow.SetActive(false);
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

            //대화 도중 범위 밖으로 나가면 대화 초기화
            dialogueText.gameObject.SetActive(false);
            dialogueWindow.SetActive(false);
            currentTalkIndex = 0;
        }

        dialogueStart();
    }

    public void dialogueStart()
    {
        if (isInRange == true)
        {
            if (!isTyping && Input.GetKeyDown(KeyCode.Space))
            {
                if (currentTalkIndex < talk.Length)
                {
                    dialogueText.gameObject.SetActive(true); //다이얼로그 텍스트 활성화
                    dialogueWindow.SetActive(true); //대화창 활성화
                    StartCoroutine(TypeText(talk[currentTalkIndex])); //타이핑 모션 시작
                }
                else
                {
                    // 대화가 모두 출력된 경우 대화창 비활성화, currentTalkIndex 초기화
                    dialogueText.gameObject.SetActive(false);
                    dialogueWindow.SetActive(false);
                    currentTalkIndex = 0;
                }
            }
        }
    }

    IEnumerator TypeText(string textToType) //타이핑 모션
    {
        isTyping = true; //타이핑 중으로 설정
        dialogueText.text = ""; //대화 텍스트 초기화
        int charCount = 0; //출력된 글자 수

        foreach (char letter in textToType) //입력된 텍스트를 한 글자씩 순회
        {
            dialogueText.text += letter; //각 글자를 다이얼로그 텍스트에 하나씩 입력
            charCount++; //한글자가 입력 될때마다 출력된 글자 수 ++

            if (charCount > 10 && letter == ' ') //10글자 이상 출력된다면
            {
                dialogueText.text += "\n"; //줄바꿈
                charCount = 0; //출력된 글자 수 
            }
            yield return new WaitForSeconds(typingSpeed); //typingSpeed만큼 대기
        }

        isTyping = false; //타이핑 종료
        currentTalkIndex++; //다음 대화 내용으로 인덱스 증가
    }
}