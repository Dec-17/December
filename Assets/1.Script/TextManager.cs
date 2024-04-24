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
    public Text talkText; //��� �ؽ�Ʈ
    public GameObject chatArrow; //��ȭ ���� ȭ��ǥ �̹���
    public GameObject chatWindow; //��ȭâ
    public GameObject windowShadow; //��ȭ ���� ����ȭ�� ����ȿ��

    public int currentTextIndex = 0;
    public float typingSpeed = 0.05f; //Ÿ���θ�� �ӵ�
    private bool isTyping = false; //Ÿ���� ��� Ȱ��ȭ ������ Ȯ��
    public int chapterNumber = 0; //���� é��
    public TextArray[] chapter; //é�� �迭

    private bool[] isChapter; //é�Ͱ� ������ �Ǿ����� Ȯ��

    //private EventSystem eventSystem; //�̺�Ʈ �ý���

    public void Start()
    {
        //���� ���� �� ��ȭâ UI ��Ȱ��ȭ
        talkText.gameObject.SetActive(false);
        chatArrow.SetActive(false);
        chatWindow.SetActive(false);
        windowShadow.SetActive(false);

        isChapter = new bool[chapter.Length]; //isChapter �迭 �ʱ�ȭ
        //eventSystem = EventSystem.current;

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
                    talkText.gameObject.SetActive(true);
                    chatArrow.SetActive(true);
                    chatWindow.SetActive(true);
                    windowShadow.SetActive(true);

                    //�̺�Ʈ �ý��� ���
                    //LockEventSystem(true);
                }
                else
                {
                    //��ȭ ���� �� ��ȭâ UI ��Ȱ��ȭ
                    talkText.gameObject.SetActive(false);
                    chatArrow.SetActive(false);
                    chatWindow.SetActive(false);
                    windowShadow.SetActive(false);

                    //�̺�Ʈ �ý��� �������
                    //LockEventSystem(false);
                }
            }
            else //Ÿ���� ����� �����̽� �Է� ��
            {
                //Ÿ���� ȿ�� ��� ����
                StopAllCoroutines();
                talkText.text = chapter[chapterNumber].talk[currentTextIndex - 1];
                chatArrow.SetActive(true);
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
                    talkText.gameObject.SetActive(true);
                    chatArrow.SetActive(true);
                    chatWindow.SetActive(true);
                    windowShadow.SetActive(true);

                    //�̺�Ʈ �ý��� ���
                    //LockEventSystem(true);
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
        if (!isChapter[1]) //é�Ͱ� ����Ǿ����� Ȯ��
        {
            chapterNumber = 1;
            currentTextIndex = 0;
            isChapter[1] = true; //é�Ͱ� ����Ǿ����� Ȯ��
        }
    }

    //private void LockEventSystem(bool lockState) //��ȭ���� �̺�Ʈ �ý��� ����
    //{
    //    if (eventSystem != null)
    //    {
    //        eventSystem.enabled = !lockState;
    //    }
    //}
}
