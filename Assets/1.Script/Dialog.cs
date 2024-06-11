using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue //下
{
    [Tooltip("캐릭터 이름")]
    public string Name;

    [Tooltip("캐릭터 이미지")]
    public Sprite Img;

    [Tooltip("대사 내용")]
    [TextArea]
    public string[] contexts;
}

[System.Serializable]
public class Chapter
{
    public Dialogue[] dialogues; //中
}

public class Dialog : MonoBehaviour
{
    [SerializeField] private Chapter[] chapters; //上

    [SerializeField] private GameObject textBox; //대화 UI 패널
    [SerializeField] private Text context; //대사를 표시 할 텍스트
    [SerializeField] private GameObject nameBox; //네임 UI 패널
    [SerializeField] private Text characterName; //캐릭터의 이름을 표시 할 텍스트
    [SerializeField] private Image characterImg; //캐릭터의 이미지 스프라이트를 담을 Image

    private int currentChapterIndex = 0; //현재 챕터 인덱스
    private int currentDialogueIndex = 0; //현재 대화 인덱스
    private int currentContextIndex = 0; //현재 대사 인덱스

    private int chapterNum; //불러올 챕터 넘버 상황에 따라 변경***********************

    private bool isTyping = false; //타이핑모션 활성화 여부
    private Coroutine typingCoroutine; //타이핑 모션 코루틴



    private void Update() //업데이트
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            chapterNum = 0;
            if (isTyping)
            {
                StopTyping(); //타이핑 모션 중이면 즉시 종료
            }
            else
            {
                DialogueStart(); //타이핑 모션 중이 아니면 다음 대화로 넘어감
            }
        }
    }

    private void ONOFF(bool _flag) //대화 UI의 활성화/비활성화 설정
    {
        textBox.SetActive(_flag);
        context.gameObject.SetActive(_flag);
        nameBox.SetActive(_flag);
        characterName.gameObject.SetActive(_flag);
        characterImg.gameObject.SetActive(_flag);
    }

    public void DialogueStart() //대화 시작
    {
        ONOFF(true); //대화 UI 활성화

        if (currentChapterIndex < chapters.Length) //현재 챕터 인덱스가 챕터 배열의 길이보다 작은지 확인 (즉, 유효한 챕터인지 확인)
        {
            Chapter chapter = chapters[chapterNum]; //현재 챕터 가져오기

            if (currentDialogueIndex < chapter.dialogues.Length) //현재 대화 인덱스가 대화 배열의 길이보다 작은지 확인 (즉, 유효한 대화인지 확인)
            {
                Dialogue dialogue = chapter.dialogues[currentDialogueIndex]; //현재 대화 가져오기

                //Dialogue의 속성들을 변수에 할당
                characterName.text = dialogue.Name; //캐릭터 이름 설정
                characterImg.sprite = dialogue.Img; //캐릭터 이미지 설정
                string[] contextTexts = dialogue.contexts; //대사 내용 배열 가져오기

                // 대사 출력
                if (currentContextIndex < contextTexts.Length) //현재 대사 인덱스가 대사 내용 배열의 길이보다 작은지 확인 (즉, 유효한 대사인지 확인)
                {
                    if (typingCoroutine != null) //현재 타이핑 코루틴이 실행 중인지 확인
                    {
                        StopCoroutine(typingCoroutine); //실행 중인 타이핑 코루틴 중지
                    }
                    typingCoroutine = StartCoroutine(TypeText(contextTexts[currentContextIndex])); // 새로운 타이핑 코루틴 시작
                    currentContextIndex++; //다음 대사로 인덱스 증가
                }
                else //현재 대사 인덱스가 대사 내용 배열의 길이보다 크거나 같으면
                {
                    currentDialogueIndex++; //다음 대화로 인덱스 증가
                    currentContextIndex = 0; //대사 인덱스 초기화

                    if (currentDialogueIndex < chapter.dialogues.Length) // 다음 대화가 유효한지 확인
                    {
                        DialogueStart(); //다음 대화로 넘어가서 다시 시작
                    }
                    else //다음 대화가 없으면
                    {
                        ONOFF(false); //대화 UI 비활성화
                        currentDialogueIndex = 0; //대화 인덱스 초기화
                        currentContextIndex = 0; //대사 인덱스 초기화
                    }
                }
            }
            else // 현재 대화 인덱스가 대화 배열의 길이보다 크거나 같으면
            {
                ONOFF(false); //대화 UI 비활성화
                currentDialogueIndex = 0; // 대화 인덱스 초기화
                currentContextIndex = 0; // 대사 인덱스 초기화
            }
        }
        else // 현재 챕터 인덱스가 챕터 배열의 길이보다 크거나 같으면
        {
            ONOFF(false); //대화 UI 비활성화
            currentChapterIndex = 0; // 챕터 인덱스 초기화
            currentDialogueIndex = 0; // 대화 인덱스 초기화
            currentContextIndex = 0; // 대사 인덱스 초기화
        }
    }

    private IEnumerator TypeText(string textToType) //타이핑 모션 실행
    {
        isTyping = true;
        context.text = "";
        foreach (char letter in textToType)
        {
            context.text += letter;
            yield return new WaitForSeconds(0.05f); //타이핑 모션 속도
        }
        isTyping = false;
    }

    private void StopTyping() //타이핑 모션 종료
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }
        isTyping = false;
        context.text = chapters[currentChapterIndex].dialogues[currentDialogueIndex].contexts[currentContextIndex - 1];
    }
}