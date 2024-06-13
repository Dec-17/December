using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DialogueState
{
    public int currentChapterIndex = 0; // ���� é�� �ε���
    public int currentDialogueIndex = 0; // ���� ��ȭ �ε���
    public int currentContextIndex = 0; // ���� ��� �ε���
}

public class Dialog : MonoBehaviour
{
    [SerializeField] private DialogueData dialogueData; // ��ũ���ͺ� ������Ʈ, ��ȭ �����͸� ��� ����
    [SerializeField] private GameObject textBox; // ��ȭ UI �г�
    [SerializeField] private Text context; // ��縦 ǥ���� �ؽ�Ʈ
    [SerializeField] private GameObject nameBox; // ĳ���� �̸��� ǥ���� UI �г�
    [SerializeField] private Text characterName; // ĳ������ �̸��� ǥ���� �ؽ�Ʈ
    [SerializeField] private Image characterImg; // ĳ������ �̹��� ��������Ʈ�� ���� Image

    private DialogueState dialogueState; // ��ȭ ���� ���� Ŭ����

    private bool isTyping = false; // Ÿ���� ��� Ȱ��ȭ ����
    private bool isDialogueActive = false; // ��ȭ Ȱ��ȭ ����
    private Coroutine typingCoroutine; // Ÿ���� ��� �ڷ�ƾ

    [SerializeField] private int currentChapterNum; // ������ é�� ��ȣ ����

    PlayerController playerController; // �÷��̾� ��Ʈ�ѷ� ���� ����*****�÷��̾� ��Ʈ�ѷ� ����*****
    Bow bow; // *****�÷��̾� ��Ʈ�ѷ� ����*****

    private void Awake()
    {
        dialogueState = new DialogueState(); // ��ȭ ���� �ʱ�ȭ

        playerController = FindObjectOfType<PlayerController>(); // �÷��̾� ��Ʈ�ѷ� ����*****�÷��̾� ��Ʈ�ѷ� ����*****
        bow = FindObjectOfType<Bow>(); // *****�÷��̾� ��Ʈ�ѷ� ����*****
    }

    private void Update() // ������Ʈ
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
        {
            if (isDialogueActive)
            {
                if (isTyping)
                {
                    StopTyping(); // Ÿ���� ��� ���̸� ��� ����
                }
                else
                {
                    DisplayNextDialogue(); // Ÿ���� ��� ���� �ƴϸ� ���� ��ȭ�� �Ѿ
                }
            }
        }
    }

    private void ONOFF(bool _flag) // ��ȭ UI�� Ȱ��ȭ/��Ȱ��ȭ ����
    {
        textBox.SetActive(_flag); // �ؽ�Ʈ �ڽ� Ȱ��ȭ/��Ȱ��ȭ
        context.gameObject.SetActive(_flag); // ��� �ؽ�Ʈ Ȱ��ȭ/��Ȱ��ȭ
        nameBox.SetActive(_flag); // ���� �ڽ� Ȱ��ȭ/��Ȱ��ȭ
        characterName.gameObject.SetActive(_flag); // ĳ���� �̸� �ؽ�Ʈ Ȱ��ȭ/��Ȱ��ȭ
        characterImg.gameObject.SetActive(_flag); // ĳ���� �̹��� Ȱ��ȭ/��Ȱ��ȭ
    }

    public void DialogueStart() // ��ȭ ����
    {
        if (isDialogueActive)
        {
            return; // �̹� ��ȭ ���̸� �ƹ� ���۵� ���� ����
        }

        isDialogueActive = true; // ��ȭ Ȱ��ȭ
        ONOFF(true); // ��ȭ UI Ȱ��ȭ
        DisplayNextDialogue(); // ù ��° ��ȭ ǥ��

        playerController.isDialogue = true; // *****�÷��̾� ��Ʈ�ѷ� ����*****
        bow.isDialogue = true; // *****�÷��̾� ��Ʈ�ѷ� ����*****

    }

    private void DisplayNextDialogue()
    {
        if (currentChapterNum < dialogueData.chapters.Length) // ������ é�� ��ȣ�� ��ȿ���� Ȯ��
        {
            Chapters chapters = dialogueData.chapters[currentChapterNum]; // ������ é�� ��������

            if (dialogueState.currentDialogueIndex < chapters.dialogues.Length)
            {
                Dialogue dialogue = chapters.dialogues[dialogueState.currentDialogueIndex];

                // Dialogue�� �Ӽ����� ������ �Ҵ�
                characterName.text = dialogue.Name; // ĳ���� �̸� ����
                characterImg.sprite = dialogue.Img; // ĳ���� �̹��� ����
                string[] contextTexts = dialogue.contexts; // ��� �ؽ�Ʈ �迭

                // ��� ���
                if (dialogueState.currentContextIndex < contextTexts.Length)
                {
                    if (typingCoroutine != null)
                    {
                        StopCoroutine(typingCoroutine); // ���� Ÿ���� ��� ����
                    }
                    typingCoroutine = StartCoroutine(TypeText(contextTexts[dialogueState.currentContextIndex])); // Ÿ���� ��� ����
                    dialogueState.currentContextIndex++; // ���� ��� �ε����� �̵�
                }
                else
                {
                    dialogueState.currentDialogueIndex++; // ���� ��ȭ �ε����� �̵�
                    dialogueState.currentContextIndex = 0; // ��� �ε��� �ʱ�ȭ

                    if (dialogueState.currentDialogueIndex < chapters.dialogues.Length)
                    {
                        DisplayNextDialogue(); // ���� ��ȭ�� �Ѿ�� �ٽ� ����
                    }
                    else
                    {
                        EndDialogue(); // ��ȭ�� ��� ������ ����
                    }
                }
            }
            else
            {
                EndDialogue(); // ��ȭ�� ��� ������ ����
            }
        }
        else
        {
            EndDialogue(); // ��� é�Ͱ� ������ ����
        }
    }

    private void EndDialogue()
    {
        ONOFF(false); // ��ȭ UI ��Ȱ��ȭ
        dialogueState.currentDialogueIndex = 0; // ���� �ʱ�ȭ
        dialogueState.currentContextIndex = 0; // ���� �ʱ�ȭ
        isDialogueActive = false; // ��ȭ ��Ȱ��ȭ

        playerController.isDialogue = false; // *****�÷��̾� ��Ʈ�ѷ� ����*****
        bow.isDialogue = false; // *****�÷��̾� ��Ʈ�ѷ� ����*****
    }

    private IEnumerator TypeText(string textToType) // Ÿ���� ��� ����
    {
        isTyping = true; // Ÿ���� ��� Ȱ��ȭ
        context.text = ""; // ��� �ؽ�Ʈ �ʱ�ȭ
        foreach (char letter in textToType)
        {
            context.text += letter; // �� ���ھ� �߰�
            yield return new WaitForSeconds(0.1f); // Ÿ���� ��� �ӵ�
        }
        isTyping = false; // Ÿ���� ��� ��Ȱ��ȭ
    }

    private void StopTyping() // Ÿ���� ��� ����
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine); // Ÿ���� ��� �ڷ�ƾ ����
            typingCoroutine = null;
        }
        isTyping = false; // Ÿ���� ��� ��Ȱ��ȭ
        // ������ ��� �ؽ�Ʈ ��ü ���
        context.text = dialogueData.chapters[currentChapterNum].dialogues[dialogueState.currentDialogueIndex].contexts[dialogueState.currentContextIndex - 1];
    }

    public void SetChapterNum(int chapterNum) // é�� ��ȣ ���� �޼���
    {
        currentChapterNum = chapterNum; // ���� é�� ��ȣ ����
        dialogueState.currentChapterIndex = chapterNum; // ��ȭ ������ ���� é�� �ε��� ����
        dialogueState.currentDialogueIndex = 0; // ��ȭ �ε��� �ʱ�ȭ
        dialogueState.currentContextIndex = 0; // ��� �ε��� �ʱ�ȭ
    }
}


// Dialog dialogScript;
// dialogScript = FindObjectOfType<Dialog>();
// dialog.SetChapterNum(0); //���� : 0�� é�ͷ� ����
// dialog.DialogueStart(); //��ȭ ����
