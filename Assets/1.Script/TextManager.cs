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
    public Text dialogueText; //다이얼로그 텍스트
    public GameObject dialogueArrow; //대화 종료 화살표 이미지
    public GameObject dialogueWindow; //대화창

    public int currentTextIndex = 0;
    public float typingSpeed = 0.05f; //타이핑모션 속도
    private bool isTyping = false; //타이핑모션 활성화 여부 확인
    public int chapterNumber = 0; //현재 챕터
    public TextArray[] chapter; //챕터 배열

    private bool[] isChapter; //챕터 실행 여부

    public void Start()
    {
        //게임 실행 시 대화창 UI 비활성화
        dialogueText.gameObject.SetActive(false);
        dialogueArrow.SetActive(false);
        dialogueWindow.SetActive(false);

        isChapter = new bool[chapter.Length]; //isChapter 배열 초기화

        for (int i = 0; i > isChapter.Length; i++)
        {
            isChapter[i] = false;
        }
    }

    private void Update()
    {
            textLoad();
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
                    dialogueText.gameObject.SetActive(true);
                    dialogueArrow.SetActive(true);
                    dialogueWindow.SetActive(true);
                }
                else
                {
                    //대화 종료 시 대화창 UI 비활성화
                    dialogueText.gameObject.SetActive(false);
                    dialogueArrow.SetActive(false);
                    dialogueWindow.SetActive(false);
                }
            }
            else //타이핑 모션중 스페이스 입력 시
            {
                //타이핑 효과 즉시 종료
                StopAllCoroutines();
                dialogueText.text = chapter[chapterNumber].talk[currentTextIndex - 1];
                dialogueArrow.SetActive(true);
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
                    dialogueText.gameObject.SetActive(true);
                    dialogueArrow.SetActive(true);
                    dialogueWindow.SetActive(true);
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
        dialogueText.text = "";
        foreach (char letter in textToType)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        dialogueArrow.SetActive(true);
        isTyping = false;
    }

    public void Chapter1()
    {
        if (!isChapter[1]) //챕터 실행 여부
        {
            chapterNumber = 1;
            currentTextIndex = 0;
            isChapter[1] = true; //챕터 실행 확인
        }
    }
}
