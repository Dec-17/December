using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

//[System.Serializable]
//public class TextArray
//{
//    public string[] talk; //대사 배열
//}

public class FarmTextManager : MonoBehaviour
{
    public Text talkText; //대사
    public GameObject npcImg; //NPC이미지
    public GameObject arrowImg; //대화 종료를 알리는 화살표 이미지
    public GameObject chatWindow; //대화창
    public GameObject windowShadow; //대화도중 화면 음영효과

    public int currentTextIndex = 0;
    public float typingSpeed = 0.05f; //타이핑모션 속도
    private bool isTyping = false; //현재 타이핑 모션이 활성화중인지 확인
    public int chapterNumber = 0; //현재 챕터
    public TextArray[] Chapter; //챕터 배열

    private bool[] isChapter; //챕터가 실행이 되었는지 체크
    private EventSystem eventSystem; //대화도중 이벤트시스템 작동을 방지

    public void Start()
    {
        // 게임을 실행하자마자 UI가 보이는 현상 방지
        npcImg.SetActive(false);
        arrowImg.SetActive(false);
        chatWindow.SetActive(false);
        talkText.gameObject.SetActive(false);
        windowShadow.SetActive(false);

        isChapter = new bool[Chapter.Length]; // isChapter 배열 초기화
        for (int i = 0; i < isChapter.Length; i++)
        {
            isChapter[i] = false;
        }

        eventSystem = EventSystem.current;

        // 씬이 실행되자마자 첫 번째 대화 시작
        StartCoroutine(StartFirstConversation());

    }

    IEnumerator StartFirstConversation()
    {
        yield return new WaitForSeconds(1f); // 대화가 시작되기 전 잠시 대기

        // 첫 번째 챕터가 실행되지 않았을 경우에만 실행
        if (!isChapter[0])
        {
            chapterNumber = 0; // 첫 번째 챕터 설정
            currentTextIndex = 0; // 대화 인덱스 초기화
            isChapter[0] = true; // 챕터 실행 플래그 설정

            // 대화 시작 시 텍스트에 타이핑 모션 효과
            StartCoroutine(TypeText(Chapter[chapterNumber].talk[currentTextIndex]));
            currentTextIndex++;

            // 대화 시작 시 대화창 UI 활성화
            npcImg.SetActive(true);
            chatWindow.SetActive(true);
            talkText.gameObject.SetActive(true);
            arrowImg.SetActive(false);
            windowShadow.SetActive(true);

            // 이벤트 시스템 잠금
            LockEventSystem(true);
        }
    }
    private void Update()
    {
        textLoad();
    }

    public void textLoad()
    {
        if (Input.GetMouseButtonDown(0)) //talk의 배열이 0번이 아니라면 클릭으로 대화 넘어감
        {
            if (!isTyping) // 타이핑모션중이 아닐 시
            {
                if (currentTextIndex < Chapter[chapterNumber].talk.Length)
                {
                    // 대화 시작 시 텍스트에 타이핑모션 효과
                    StartCoroutine(TypeText(Chapter[chapterNumber].talk[currentTextIndex]));
                    currentTextIndex++;

                    // 대화 시작 시 대화창 UI 활성화
                    npcImg.SetActive(true);
                    chatWindow.SetActive(true);
                    talkText.gameObject.SetActive(true);
                    arrowImg.SetActive(false);
                    windowShadow.SetActive(true);

                    // 이벤트 시스템 잠금
                    LockEventSystem(true);
                }
                else
                {
                    // 대화 종료 시 대화창 UI 비활성화
                    npcImg.SetActive(false);
                    arrowImg.SetActive(false);
                    chatWindow.SetActive(false);
                    talkText.gameObject.SetActive(false);
                    windowShadow.SetActive(false);

                    // 이벤트 시스템 잠금 해제
                    LockEventSystem(false);
                }
            }
            else // 타이핑모션 도중 클릭 시
            {
                // 클릭 입력으로 타이핑 즉시 종료
                StopAllCoroutines();
                talkText.text = Chapter[chapterNumber].talk[currentTextIndex - 1];
                arrowImg.SetActive(true);
                isTyping = false;
            }
        }
        else // talk의 배열이 0번째인 경우에는 바로 실행
        {
            if (!isTyping && currentTextIndex == 0)
            {
                if (Chapter[chapterNumber].talk.Length > 0)
                {
                    StartCoroutine(TypeText(Chapter[chapterNumber].talk[currentTextIndex]));
                    currentTextIndex++;

                    // 대화 시작 시 대화창 UI 활성화
                    npcImg.SetActive(true);
                    chatWindow.SetActive(true);
                    talkText.gameObject.SetActive(true);
                    arrowImg.SetActive(false);
                    windowShadow.SetActive(true);

                    // 이벤트 시스템 잠금
                    LockEventSystem(true);
                }
            }
        }
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

    public void Chapter2()
    {
        if (isChapter[1] && !isChapter[2]) // Chapter1이 실행되었고 Chapter2가 실행되지 않았을 때
        {
            chapterNumber = 2;
            currentTextIndex = 0;
            isChapter[2] = true; // 챕터가 실행되었음을 확인
        }
    }

    public void Chapter3()
    {
        if (isChapter[2] && !isChapter[3]) // Chapter1이 실행되었고 Chapter3이 실행되지 않았을 때
        {
            chapterNumber = 3;
            currentTextIndex = 0;
            isChapter[3] = true; // 챕터가 실행되었음을 확인
        }
    }

    public void Chapter4()
    {
        if (isChapter[3] && !isChapter[4]) // Chapter2가 실행되었고 Chapter4가 실행되지 않았을 때
        {
            chapterNumber = 4;
            currentTextIndex = 0;
            isChapter[4] = true; // 챕터가 실행되었음을 확인
        }
    }

    public void Chapter5()
    {
        if (isChapter[4] && !isChapter[5]) // Chapter3이 실행되었고 Chapter5가 실행되지 않았을 때
        {
            chapterNumber = 5;
            currentTextIndex = 0;
            isChapter[5] = true; // 챕터가 실행되었음을 확인
        }
    }

    public void Chapter6()
    {
        if (isChapter[5] && !isChapter[6]) // Chapter4가 실행되었고 Chapter6이 실행되지 않았을 때
        {
            chapterNumber = 6;
            currentTextIndex = 0;
            isChapter[6] = true; // 챕터가 실행되었음을 확인
        }
    }

    public void Chapter7()
    {
        if (isChapter[6] && !isChapter[7]) // Chapter5가 실행되었고 Chapter7이 실행되지 않았을 때
        {
            chapterNumber = 7;
            currentTextIndex = 0;
            isChapter[7] = true; // 챕터가 실행되었음을 확인
        }
    }

    public void Chapter8()
    {
        if (isChapter[7] && !isChapter[8]) // Chapter6이 실행되었고 Chapter8이 실행되지 않았을 때
        {
            chapterNumber = 8;
            currentTextIndex = 0;
            isChapter[8] = true; // 챕터가 실행되었음을 확인
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
        arrowImg.SetActive(true);
        isTyping = false;
    }

    private void LockEventSystem(bool lockState) //대화도중 이벤트 시스템 방지
    {
        if (eventSystem != null)
        {
            eventSystem.enabled = !lockState;
        }
    }
}
