using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager1 : MonoBehaviour
{
    public Text dialogueText; // 다이얼로그 텍스트
    public GameObject dialogueWindow; // 대화창

    public int currentTextIndex = 0; // 현재 텍스트 인덱스
    public float typingSpeed = 0.05f; // 타이핑 속도
    private bool isTyping = false; // 타이핑 중인지 여부
    public int chapterNumber = 0; // 현재 챕터 번호
    public TextArray[] chapter; // 챕터 배열

    private bool[] isChapter; // 각 챕터 실행 여부를 저장하는 배열

    // 게임 시작 시 초기화
    public void Start()
    {
        // 대화창 UI를 비활성화
        dialogueText.gameObject.SetActive(false);
        dialogueWindow.SetActive(false);

        // 각 챕터의 실행 여부를 저장할 배열을 초기화
        isChapter = new bool[chapter.Length];

        // 배열을 모두 false로 초기화
        for (int i = 0; i < isChapter.Length; i++)
        {
            isChapter[i] = false;
        }
    }

    // 매 프레임마다 실행되는 업데이트 함수
    private void Update()
    {
        // 텍스트 로드 함수 호출
        textLoad();
    }

    // 텍스트를 로드하는 함수
    public void textLoad()
    {
        // 스페이스 키 입력 감지
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 타이핑 중이 아닐 때
            if (!isTyping)
            {
                // 현재 챕터의 텍스트 인덱스가 배열 길이보다 작을 때
                if (currentTextIndex < chapter[chapterNumber].talk.Length)
                {
                    // 타이핑 효과를 적용하는 코루틴 시작
                    StartCoroutine(TypeText(chapter[chapterNumber].talk[currentTextIndex]));
                    currentTextIndex++; // 텍스트 인덱스 증가

                    // 대화창 UI 활성화
                    dialogueText.gameObject.SetActive(true);
                    dialogueWindow.SetActive(true);
                }
                else
                {
                    // 대화가 종료되었을 때 대화창 UI를 비활성화
                    dialogueText.gameObject.SetActive(false);
                    dialogueWindow.SetActive(false);
                }
            }
            else // 타이핑 중일 때
            {
                // 타이핑 효과를 즉시 종료하고 현재 텍스트를 모두 표시
                StopAllCoroutines();
                dialogueText.text = chapter[chapterNumber].talk[currentTextIndex - 1];
                isTyping = false;
            }
        }
        else // 대화가 시작되지 않은 상태일 때
        {
            if (!isTyping && currentTextIndex == 0)
            {
                // 현재 챕터의 텍스트 배열의 길이가 0보다 크면 대화를 시작
                if (chapter[chapterNumber].talk.Length > 0)
                {
                    StartCoroutine(TypeText(chapter[chapterNumber].talk[currentTextIndex]));
                    currentTextIndex++;

                    // 대화창 UI 활성화
                    dialogueText.gameObject.SetActive(true);
                    dialogueWindow.SetActive(true);
                }
            }
        }
    }

    // 타이핑 효과를 적용하는 코루틴 함수
    IEnumerator TypeText(string textToType)
    {
        // 입력된 텍스트가 null이 아닐 때
        if (textToType != null)
        {
            isTyping = true; // 타이핑 중임을 나타냄
            dialogueText.text = ""; // 대화 텍스트 초기화

            // 입력된 텍스트를 한 글자씩 순회하면서 대화창에 표시
            foreach (char letter in textToType)
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(typingSpeed); // 일정 시간 간격으로 텍스트를 표시
            }

            isTyping = false; // 타이핑 종료
        }
    }

    // 챕터를 변경하는 함수
    public void Chapter1()
    {
        // 해당 챕터가 실행되지 않았을 때 실행
        if (!isChapter[1])
        {
            chapterNumber = 1; // 현재 챕터 번호 설정
            currentTextIndex = 0; // 텍스트 인덱스 초기화
            isChapter[1] = true; // 해당 챕터가 실행됨을 표시
        }
    }
}
