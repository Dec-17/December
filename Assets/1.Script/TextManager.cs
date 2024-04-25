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
    public string[] talk; //��� �迭
}

public class TextManager : MonoBehaviour
{
    public Text dialogueText; //���̾�α� �ؽ�Ʈ
    public GameObject dialogueArrow; //��ȭ ���� ȭ��ǥ �̹���
    public GameObject dialogueWindow; //��ȭâ

    public int currentTextIndex = 0;
    public float typingSpeed = 0.05f; //Ÿ���θ�� �ӵ�
    private bool isTyping = false; //Ÿ���θ�� Ȱ��ȭ ���� Ȯ��
    public int chapterNumber = 0; //���� é��
    public TextArray[] chapter; //é�� �迭

    private bool[] isChapter; //é�� ���� ����

    public void Start()
    {
        //���� ���� �� ��ȭâ UI ��Ȱ��ȭ
        dialogueText.gameObject.SetActive(false);
        dialogueArrow.SetActive(false);
        dialogueWindow.SetActive(false);

        isChapter = new bool[chapter.Length]; //isChapter �迭 �ʱ�ȭ

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
            if (!isTyping) //Ÿ���� ������� �ƴ� ��
            {
                if (currentTextIndex < chapter[chapterNumber].talk.Length)
                {
                    //��ȭ ���� �� Ÿ���θ�� ȿ��
                    StartCoroutine(TypeText(chapter[chapterNumber].talk[currentTextIndex]));
                    currentTextIndex++;

                    //��ȭ ���� �� ��ȭâ UI Ȱ��ȭ
                    dialogueText.gameObject.SetActive(true);
                    dialogueArrow.SetActive(true);
                    dialogueWindow.SetActive(true);
                }
                else
                {
                    //��ȭ ���� �� ��ȭâ UI ��Ȱ��ȭ
                    dialogueText.gameObject.SetActive(false);
                    dialogueArrow.SetActive(false);
                    dialogueWindow.SetActive(false);
                }
            }
            else //Ÿ���� ����� �����̽� �Է� ��
            {
                //Ÿ���� ȿ�� ��� ����
                StopAllCoroutines();
                dialogueText.text = chapter[chapterNumber].talk[currentTextIndex - 1];
                dialogueArrow.SetActive(true);
                isTyping = false;
            }
        }
        else //talk�� �迭�� 0�� ��� �ٷ� ����
        {
            if (!isTyping && currentTextIndex == 0)
            {
                if (chapter[chapterNumber].talk.Length > 0)
                {
                    StartCoroutine(TypeText(chapter[chapterNumber].talk[currentTextIndex]));
                    currentTextIndex++;

                    //��ȭ ���� �� ��ȭâ UI Ȱ��ȭ
                    dialogueText.gameObject.SetActive(true);
                    dialogueArrow.SetActive(true);
                    dialogueWindow.SetActive(true);
                }
            }
        }
    }
    IEnumerator TypeText(string textToType) //Ÿ���θ��
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
        if (!isChapter[1]) //é�� ���� ����
        {
            chapterNumber = 1;
            currentTextIndex = 0;
            isChapter[1] = true; //é�� ���� Ȯ��
        }
    }
}
