using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;

[System.Serializable]
public class TextArray
{
    public string[] talk; //대사 배열
}

public class TextManager : MonoBehaviour
{
    public Text talkText; //대사 텍스트
    public GameObject chatArrow; //대화 종료 화살표 이미지
    public GameObject chatWindow; //대화창
    public GameObject windowShadow; //대화 도중 게임화면 음영효과

    public int currentTextIndex = 0;
    public float typingSpeed = 0.05f; //타이핑모션 속도
    private bool isTyping = false; //타이핑 모션 활성화 중인지 확인
    public int chapterNumber = 0; //현재 챕터
    public TextArray[] chapter; //챕터 배열

    private bool[] isChapter; //챕터가 실행이 되었는지 확인

    //private EventSystem eventSystem; //이벤트 시스템

    public GameObject talkTrue; //대화 가능 범위에 들어왔는지 표시
    public bool isInRange = false; //대화 가능 범위에 들어왔는지 확인하는 값
    public float talkLength = 3; //대화 가능 범위


    public void Start()
    {
        //게임 실행 시 대화창 UI 비활성화
        talkText.gameObject.SetActive(false);
        chatArrow.SetActive(false);
        chatWindow.SetActive(false);
        windowShadow.SetActive(false);

        isChapter = new bool[chapter.Length]; //isChapter 배열 초기화
        //eventSystem = EventSystem.current;

        for (int i = 0; i > isChapter.Length; i++)
        {
            isChapter[i] = false;
        }
    }

    private void Update()
    {
        if(isInRange == true)
        {
            textLoad();
        }
        

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
            //대화 가능 표시 활성화
            //talkTrue 오브젝트의 알파값을 1로 설정
            Color color = talkTrue.GetComponent<Renderer>().material.color;
            color.a = 1f;
            talkTrue.GetComponent<Renderer>().material.color = color;
        }
        else
        {
            Debug.Log("대화불가능");
            //대화 가능 표시 비활성화
            //범위 밖에 있다면 talkTrue 오브젝트의 알파값을 0으로 설정
            Color color = talkTrue.GetComponent<Renderer>().material.color;
            color.a = 0f;
            talkTrue.GetComponent<Renderer>().material.color = color;
        }
    }

    public void textLoad()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isTyping) //타이핑 모션중이 아닐 시
            {
                if (currentTextIndex < chapter[chapterNumber].talk.Length)
                {
                    //대화 시작 시 타이핑모션 효과
                    StartCoroutine(TypeText(chapter[chapterNumber].talk[currentTextIndex]));
                    currentTextIndex++;

                    //대화 시작 시 대화창 UI 활성화
                    talkText.gameObject.SetActive(true);
                    chatArrow.SetActive(true);
                    chatWindow.SetActive(true);
                    windowShadow.SetActive(true);

                    //이벤트 시스템 잠금
                    //LockEventSystem(true);

                    //대화 가능 표시 비활성화
                    //범위 밖에 있다면 talkTrue 오브젝트의 알파값을 0으로 설정
                    Color color = talkTrue.GetComponent<Renderer>().material.color;
                    color.a = 0f;
                    talkTrue.GetComponent<Renderer>().material.color = color;
                }
                else
                {
                    //대화 종료 시 대화창 UI 비활성화
                    talkText.gameObject.SetActive(false);
                    chatArrow.SetActive(false);
                    chatWindow.SetActive(false);
                    windowShadow.SetActive(false);

                    //이벤트 시스템 잠금해제
                    //LockEventSystem(false);

                    //대화 가능 표시 활성화
                    //talkTrue 오브젝트의 알파값을 1로 설정
                    Color color = talkTrue.GetComponent<Renderer>().material.color;
                    color.a = 1f;
                    talkTrue.GetComponent<Renderer>().material.color = color;
                }
            }
            else //타이핑 모션중 스페이스 입력 시
            {
                //타이핑 효과 즉시 종료
                StopAllCoroutines();
                talkText.text = chapter[chapterNumber].talk[currentTextIndex - 1];
                chatArrow.SetActive(true);
                isTyping = false;
            }
        }
        else //talk의 배열이 0일 경우 바로 실행
        {
            if (!isTyping && currentTextIndex == 0)
            {
                if (chapter[chapterNumber].talk.Length > 0)
                {
                    StartCoroutine(TypeText(chapter[chapterNumber].talk[currentTextIndex]));
                    currentTextIndex++;

                    //대화 시작 시 대화창 UI 활성화
                    talkText.gameObject.SetActive(true);
                    chatArrow.SetActive(true);
                    chatWindow.SetActive(true);
                    windowShadow.SetActive(true);

                    //이벤트 시스템 잠금
                    //LockEventSystem(true);
                }
            }
        }
    }
    IEnumerator TypeText(string textToType) //타이핑모션
    {
        if (textToType == null)
        {
            yield return null;
        }
        isTyping = true;
        talkText.text = "";
        foreach (char letter in textToType)
        {
            talkText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        chatArrow.SetActive(true);
        isTyping = false;
    }

    public void Chapter1()
    {
        if (!isChapter[1]) //챕터가 실행되었는지 확인
        {
            chapterNumber = 1;
            currentTextIndex = 0;
            isChapter[1] = true; //챕터가 실행되었음을 확인
        }
    }

    //private void LockEventSystem(bool lockState) //대화도중 이벤트 시스템 방지
    //{
    //    if (eventSystem != null)
    //    {
    //        eventSystem.enabled = !lockState;
    //    }
    //}
}
