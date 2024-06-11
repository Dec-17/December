using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue //��
{
    [Tooltip("ĳ���� �̸�")]
    public string Name;

    [Tooltip("ĳ���� �̹���")]
    public Sprite Img;

    [Tooltip("��� ����")]
    [TextArea]
    public string[] contexts;
}

[System.Serializable]
public class Chapter
{
    public Dialogue[] dialogues; //��
}

public class Dialog : MonoBehaviour
{
    [SerializeField] private Chapter[] chapters; //߾

    [SerializeField] private GameObject textBox; //��ȭ UI �г�
    [SerializeField] private Text context; //��縦 ǥ�� �� �ؽ�Ʈ
    [SerializeField] private GameObject nameBox; //���� UI �г�
    [SerializeField] private Text characterName; //ĳ������ �̸��� ǥ�� �� �ؽ�Ʈ
    [SerializeField] private Image characterImg; //ĳ������ �̹��� ��������Ʈ�� ���� Image

    private int currentChapterIndex = 0; //���� é�� �ε���
    private int currentDialogueIndex = 0; //���� ��ȭ �ε���
    private int currentContextIndex = 0; //���� ��� �ε���

    private int chapterNum; //�ҷ��� é�� �ѹ� ��Ȳ�� ���� ����***********************

    private bool isTyping = false; //Ÿ���θ�� Ȱ��ȭ ����
    private Coroutine typingCoroutine; //Ÿ���� ��� �ڷ�ƾ



    private void Update() //������Ʈ
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            chapterNum = 0;
            if (isTyping)
            {
                StopTyping(); //Ÿ���� ��� ���̸� ��� ����
            }
            else
            {
                DialogueStart(); //Ÿ���� ��� ���� �ƴϸ� ���� ��ȭ�� �Ѿ
            }
        }
    }

    private void ONOFF(bool _flag) //��ȭ UI�� Ȱ��ȭ/��Ȱ��ȭ ����
    {
        textBox.SetActive(_flag);
        context.gameObject.SetActive(_flag);
        nameBox.SetActive(_flag);
        characterName.gameObject.SetActive(_flag);
        characterImg.gameObject.SetActive(_flag);
    }

    public void DialogueStart() //��ȭ ����
    {
        ONOFF(true); //��ȭ UI Ȱ��ȭ

        if (currentChapterIndex < chapters.Length) //���� é�� �ε����� é�� �迭�� ���̺��� ������ Ȯ�� (��, ��ȿ�� é������ Ȯ��)
        {
            Chapter chapter = chapters[chapterNum]; //���� é�� ��������

            if (currentDialogueIndex < chapter.dialogues.Length) //���� ��ȭ �ε����� ��ȭ �迭�� ���̺��� ������ Ȯ�� (��, ��ȿ�� ��ȭ���� Ȯ��)
            {
                Dialogue dialogue = chapter.dialogues[currentDialogueIndex]; //���� ��ȭ ��������

                //Dialogue�� �Ӽ����� ������ �Ҵ�
                characterName.text = dialogue.Name; //ĳ���� �̸� ����
                characterImg.sprite = dialogue.Img; //ĳ���� �̹��� ����
                string[] contextTexts = dialogue.contexts; //��� ���� �迭 ��������

                // ��� ���
                if (currentContextIndex < contextTexts.Length) //���� ��� �ε����� ��� ���� �迭�� ���̺��� ������ Ȯ�� (��, ��ȿ�� ������� Ȯ��)
                {
                    if (typingCoroutine != null) //���� Ÿ���� �ڷ�ƾ�� ���� ������ Ȯ��
                    {
                        StopCoroutine(typingCoroutine); //���� ���� Ÿ���� �ڷ�ƾ ����
                    }
                    typingCoroutine = StartCoroutine(TypeText(contextTexts[currentContextIndex])); // ���ο� Ÿ���� �ڷ�ƾ ����
                    currentContextIndex++; //���� ���� �ε��� ����
                }
                else //���� ��� �ε����� ��� ���� �迭�� ���̺��� ũ�ų� ������
                {
                    currentDialogueIndex++; //���� ��ȭ�� �ε��� ����
                    currentContextIndex = 0; //��� �ε��� �ʱ�ȭ

                    if (currentDialogueIndex < chapter.dialogues.Length) // ���� ��ȭ�� ��ȿ���� Ȯ��
                    {
                        DialogueStart(); //���� ��ȭ�� �Ѿ�� �ٽ� ����
                    }
                    else //���� ��ȭ�� ������
                    {
                        ONOFF(false); //��ȭ UI ��Ȱ��ȭ
                        currentDialogueIndex = 0; //��ȭ �ε��� �ʱ�ȭ
                        currentContextIndex = 0; //��� �ε��� �ʱ�ȭ
                    }
                }
            }
            else // ���� ��ȭ �ε����� ��ȭ �迭�� ���̺��� ũ�ų� ������
            {
                ONOFF(false); //��ȭ UI ��Ȱ��ȭ
                currentDialogueIndex = 0; // ��ȭ �ε��� �ʱ�ȭ
                currentContextIndex = 0; // ��� �ε��� �ʱ�ȭ
            }
        }
        else // ���� é�� �ε����� é�� �迭�� ���̺��� ũ�ų� ������
        {
            ONOFF(false); //��ȭ UI ��Ȱ��ȭ
            currentChapterIndex = 0; // é�� �ε��� �ʱ�ȭ
            currentDialogueIndex = 0; // ��ȭ �ε��� �ʱ�ȭ
            currentContextIndex = 0; // ��� �ε��� �ʱ�ȭ
        }
    }

    private IEnumerator TypeText(string textToType) //Ÿ���� ��� ����
    {
        isTyping = true;
        context.text = "";
        foreach (char letter in textToType)
        {
            context.text += letter;
            yield return new WaitForSeconds(0.05f); //Ÿ���� ��� �ӵ�
        }
        isTyping = false;
    }

    private void StopTyping() //Ÿ���� ��� ����
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