using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DialogueState
{
    public int currentChapterIndex = 0; // 현재 챕터 인덱스
    public int currentDialogueIndex = 0; // 현재 대화 인덱스
    public int currentContextIndex = 0; // 현재 대사 인덱스
}

public class Dialog : MonoBehaviour
{
    [SerializeField] private DialogueData dialogueData; // 스크립터블 오브젝트, 대화 데이터를 담고 있음
    [SerializeField] private GameObject textBox; // 대화 UI 패널
    [SerializeField] private Text context; // 대사를 표시할 텍스트
    [SerializeField] private GameObject nameBox; // 캐릭터 이름을 표시할 UI 패널
    [SerializeField] private Text characterName; // 캐릭터의 이름을 표시할 텍스트
    [SerializeField] private Image characterImg; // 캐릭터의 이미지 스프라이트를 담을 Image

    private DialogueState dialogueState; // 대화 상태 저장 클래스

    private bool isTyping = false; // 타이핑 모션 활성화 여부
    private bool isDialogueActive = false; // 대화 활성화 여부
    private Coroutine typingCoroutine; // 타이핑 모션 코루틴

    [SerializeField] private int currentChapterNum; // 지정된 챕터 번호 변수

    PlayerController playerController; // 플레이어 컨트롤러 참조 변수*****플레이어 컨트롤러 관련*****
    Bow bow; // *****플레이어 컨트롤러 관련*****

    private void Awake()
    {
        dialogueState = new DialogueState(); // 대화 상태 초기화

        playerController = FindObjectOfType<PlayerController>(); // 플레이어 컨트롤러 참조*****플레이어 컨트롤러 관련*****
        bow = FindObjectOfType<Bow>(); // *****플레이어 컨트롤러 관련*****
    }

    private void Update() // 업데이트
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
        {
            if (isDialogueActive)
            {
                if (isTyping)
                {
                    StopTyping(); // 타이핑 모션 중이면 즉시 종료
                }
                else
                {
                    DisplayNextDialogue(); // 타이핑 모션 중이 아니면 다음 대화로 넘어감
                }
            }
        }
    }

    private void ONOFF(bool _flag) // 대화 UI의 활성화/비활성화 설정
    {
        textBox.SetActive(_flag); // 텍스트 박스 활성화/비활성화
        context.gameObject.SetActive(_flag); // 대사 텍스트 활성화/비활성화
        nameBox.SetActive(_flag); // 네임 박스 활성화/비활성화
        characterName.gameObject.SetActive(_flag); // 캐릭터 이름 텍스트 활성화/비활성화
        characterImg.gameObject.SetActive(_flag); // 캐릭터 이미지 활성화/비활성화
    }

    public void DialogueStart() // 대화 시작
    {
        if (isDialogueActive)
        {
            return; // 이미 대화 중이면 아무 동작도 하지 않음
        }

        isDialogueActive = true; // 대화 활성화
        ONOFF(true); // 대화 UI 활성화
        DisplayNextDialogue(); // 첫 번째 대화 표시

        playerController.isDialogue = true; // *****플레이어 컨트롤러 관련*****
        bow.isDialogue = true; // *****플레이어 컨트롤러 관련*****

    }

    private void DisplayNextDialogue()
    {
        if (currentChapterNum < dialogueData.chapters.Length) // 지정된 챕터 번호가 유효한지 확인
        {
            Chapters chapters = dialogueData.chapters[currentChapterNum]; // 지정된 챕터 가져오기

            if (dialogueState.currentDialogueIndex < chapters.dialogues.Length)
            {
                Dialogue dialogue = chapters.dialogues[dialogueState.currentDialogueIndex];

                // Dialogue의 속성들을 변수에 할당
                characterName.text = dialogue.Name; // 캐릭터 이름 설정
                characterImg.sprite = dialogue.Img; // 캐릭터 이미지 설정
                string[] contextTexts = dialogue.contexts; // 대사 텍스트 배열

                // 대사 출력
                if (dialogueState.currentContextIndex < contextTexts.Length)
                {
                    if (typingCoroutine != null)
                    {
                        StopCoroutine(typingCoroutine); // 이전 타이핑 모션 중지
                    }
                    typingCoroutine = StartCoroutine(TypeText(contextTexts[dialogueState.currentContextIndex])); // 타이핑 모션 시작
                    dialogueState.currentContextIndex++; // 다음 대사 인덱스로 이동
                }
                else
                {
                    dialogueState.currentDialogueIndex++; // 다음 대화 인덱스로 이동
                    dialogueState.currentContextIndex = 0; // 대사 인덱스 초기화

                    if (dialogueState.currentDialogueIndex < chapters.dialogues.Length)
                    {
                        DisplayNextDialogue(); // 다음 대화로 넘어가서 다시 시작
                    }
                    else
                    {
                        EndDialogue(); // 대화가 모두 끝나면 종료
                    }
                }
            }
            else
            {
                EndDialogue(); // 대화가 모두 끝나면 종료
            }
        }
        else
        {
            EndDialogue(); // 모든 챕터가 끝나면 종료
        }
    }

    private void EndDialogue()
    {
        ONOFF(false); // 대화 UI 비활성화
        dialogueState.currentDialogueIndex = 0; // 변수 초기화
        dialogueState.currentContextIndex = 0; // 변수 초기화
        isDialogueActive = false; // 대화 비활성화

        playerController.isDialogue = false; // *****플레이어 컨트롤러 관련*****
        bow.isDialogue = false; // *****플레이어 컨트롤러 관련*****
    }

    private IEnumerator TypeText(string textToType) // 타이핑 모션 실행
    {
        isTyping = true; // 타이핑 모션 활성화
        context.text = ""; // 대사 텍스트 초기화
        foreach (char letter in textToType)
        {
            context.text += letter; // 한 글자씩 추가
            yield return new WaitForSeconds(0.1f); // 타이핑 모션 속도
        }
        isTyping = false; // 타이핑 모션 비활성화
    }

    private void StopTyping() // 타이핑 모션 종료
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine); // 타이핑 모션 코루틴 중지
            typingCoroutine = null;
        }
        isTyping = false; // 타이핑 모션 비활성화
        // 마지막 대사 텍스트 전체 출력
        context.text = dialogueData.chapters[currentChapterNum].dialogues[dialogueState.currentDialogueIndex].contexts[dialogueState.currentContextIndex - 1];
    }

    public void SetChapterNum(int chapterNum) // 챕터 번호 설정 메서드
    {
        currentChapterNum = chapterNum; // 현재 챕터 번호 설정
        dialogueState.currentChapterIndex = chapterNum; // 대화 상태의 현재 챕터 인덱스 설정
        dialogueState.currentDialogueIndex = 0; // 대화 인덱스 초기화
        dialogueState.currentContextIndex = 0; // 대사 인덱스 초기화
    }
}


// Dialog dialogScript;
// dialogScript = FindObjectOfType<Dialog>();
// dialog.SetChapterNum(0); //예시 : 0번 챕터로 변경
// dialog.DialogueStart(); //대화 시작
