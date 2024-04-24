using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

//[System.Serializable]
//public class TextArray
//{
//    public string[] talk; //��� �迭
//}

public class FarmTextManager : MonoBehaviour
{
    public Text talkText; //���
    public GameObject npcImg; //NPC�̹���
    public GameObject arrowImg; //��ȭ ���Ḧ �˸��� ȭ��ǥ �̹���
    public GameObject chatWindow; //��ȭâ
    public GameObject windowShadow; //��ȭ���� ȭ�� ����ȿ��

    public int currentTextIndex = 0;
    public float typingSpeed = 0.05f; //Ÿ���θ�� �ӵ�
    private bool isTyping = false; //���� Ÿ���� ����� Ȱ��ȭ������ Ȯ��
    public int chapterNumber = 0; //���� é��
    public TextArray[] Chapter; //é�� �迭

    private bool[] isChapter; //é�Ͱ� ������ �Ǿ����� üũ
    private EventSystem eventSystem; //��ȭ���� �̺�Ʈ�ý��� �۵��� ����

    public void Start()
    {
        // ������ �������ڸ��� UI�� ���̴� ���� ����
        npcImg.SetActive(false);
        arrowImg.SetActive(false);
        chatWindow.SetActive(false);
        talkText.gameObject.SetActive(false);
        windowShadow.SetActive(false);

        isChapter = new bool[Chapter.Length]; // isChapter �迭 �ʱ�ȭ
        for (int i = 0; i < isChapter.Length; i++)
        {
            isChapter[i] = false;
        }

        eventSystem = EventSystem.current;

        // ���� ������ڸ��� ù ��° ��ȭ ����
        StartCoroutine(StartFirstConversation());

    }

    IEnumerator StartFirstConversation()
    {
        yield return new WaitForSeconds(1f); // ��ȭ�� ���۵Ǳ� �� ��� ���

        // ù ��° é�Ͱ� ������� �ʾ��� ��쿡�� ����
        if (!isChapter[0])
        {
            chapterNumber = 0; // ù ��° é�� ����
            currentTextIndex = 0; // ��ȭ �ε��� �ʱ�ȭ
            isChapter[0] = true; // é�� ���� �÷��� ����

            // ��ȭ ���� �� �ؽ�Ʈ�� Ÿ���� ��� ȿ��
            StartCoroutine(TypeText(Chapter[chapterNumber].talk[currentTextIndex]));
            currentTextIndex++;

            // ��ȭ ���� �� ��ȭâ UI Ȱ��ȭ
            npcImg.SetActive(true);
            chatWindow.SetActive(true);
            talkText.gameObject.SetActive(true);
            arrowImg.SetActive(false);
            windowShadow.SetActive(true);

            // �̺�Ʈ �ý��� ���
            LockEventSystem(true);
        }
    }
    private void Update()
    {
        textLoad();
    }

    public void textLoad()
    {
        if (Input.GetMouseButtonDown(0)) //talk�� �迭�� 0���� �ƴ϶�� Ŭ������ ��ȭ �Ѿ
        {
            if (!isTyping) // Ÿ���θ������ �ƴ� ��
            {
                if (currentTextIndex < Chapter[chapterNumber].talk.Length)
                {
                    // ��ȭ ���� �� �ؽ�Ʈ�� Ÿ���θ�� ȿ��
                    StartCoroutine(TypeText(Chapter[chapterNumber].talk[currentTextIndex]));
                    currentTextIndex++;

                    // ��ȭ ���� �� ��ȭâ UI Ȱ��ȭ
                    npcImg.SetActive(true);
                    chatWindow.SetActive(true);
                    talkText.gameObject.SetActive(true);
                    arrowImg.SetActive(false);
                    windowShadow.SetActive(true);

                    // �̺�Ʈ �ý��� ���
                    LockEventSystem(true);
                }
                else
                {
                    // ��ȭ ���� �� ��ȭâ UI ��Ȱ��ȭ
                    npcImg.SetActive(false);
                    arrowImg.SetActive(false);
                    chatWindow.SetActive(false);
                    talkText.gameObject.SetActive(false);
                    windowShadow.SetActive(false);

                    // �̺�Ʈ �ý��� ��� ����
                    LockEventSystem(false);
                }
            }
            else // Ÿ���θ�� ���� Ŭ�� ��
            {
                // Ŭ�� �Է����� Ÿ���� ��� ����
                StopAllCoroutines();
                talkText.text = Chapter[chapterNumber].talk[currentTextIndex - 1];
                arrowImg.SetActive(true);
                isTyping = false;
            }
        }
        else // talk�� �迭�� 0��°�� ��쿡�� �ٷ� ����
        {
            if (!isTyping && currentTextIndex == 0)
            {
                if (Chapter[chapterNumber].talk.Length > 0)
                {
                    StartCoroutine(TypeText(Chapter[chapterNumber].talk[currentTextIndex]));
                    currentTextIndex++;

                    // ��ȭ ���� �� ��ȭâ UI Ȱ��ȭ
                    npcImg.SetActive(true);
                    chatWindow.SetActive(true);
                    talkText.gameObject.SetActive(true);
                    arrowImg.SetActive(false);
                    windowShadow.SetActive(true);

                    // �̺�Ʈ �ý��� ���
                    LockEventSystem(true);
                }
            }
        }
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

    public void Chapter2()
    {
        if (isChapter[1] && !isChapter[2]) // Chapter1�� ����Ǿ��� Chapter2�� ������� �ʾ��� ��
        {
            chapterNumber = 2;
            currentTextIndex = 0;
            isChapter[2] = true; // é�Ͱ� ����Ǿ����� Ȯ��
        }
    }

    public void Chapter3()
    {
        if (isChapter[2] && !isChapter[3]) // Chapter1�� ����Ǿ��� Chapter3�� ������� �ʾ��� ��
        {
            chapterNumber = 3;
            currentTextIndex = 0;
            isChapter[3] = true; // é�Ͱ� ����Ǿ����� Ȯ��
        }
    }

    public void Chapter4()
    {
        if (isChapter[3] && !isChapter[4]) // Chapter2�� ����Ǿ��� Chapter4�� ������� �ʾ��� ��
        {
            chapterNumber = 4;
            currentTextIndex = 0;
            isChapter[4] = true; // é�Ͱ� ����Ǿ����� Ȯ��
        }
    }

    public void Chapter5()
    {
        if (isChapter[4] && !isChapter[5]) // Chapter3�� ����Ǿ��� Chapter5�� ������� �ʾ��� ��
        {
            chapterNumber = 5;
            currentTextIndex = 0;
            isChapter[5] = true; // é�Ͱ� ����Ǿ����� Ȯ��
        }
    }

    public void Chapter6()
    {
        if (isChapter[5] && !isChapter[6]) // Chapter4�� ����Ǿ��� Chapter6�� ������� �ʾ��� ��
        {
            chapterNumber = 6;
            currentTextIndex = 0;
            isChapter[6] = true; // é�Ͱ� ����Ǿ����� Ȯ��
        }
    }

    public void Chapter7()
    {
        if (isChapter[6] && !isChapter[7]) // Chapter5�� ����Ǿ��� Chapter7�� ������� �ʾ��� ��
        {
            chapterNumber = 7;
            currentTextIndex = 0;
            isChapter[7] = true; // é�Ͱ� ����Ǿ����� Ȯ��
        }
    }

    public void Chapter8()
    {
        if (isChapter[7] && !isChapter[8]) // Chapter6�� ����Ǿ��� Chapter8�� ������� �ʾ��� ��
        {
            chapterNumber = 8;
            currentTextIndex = 0;
            isChapter[8] = true; // é�Ͱ� ����Ǿ����� Ȯ��
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
        arrowImg.SetActive(true);
        isTyping = false;
    }

    private void LockEventSystem(bool lockState) //��ȭ���� �̺�Ʈ �ý��� ����
    {
        if (eventSystem != null)
        {
            eventSystem.enabled = !lockState;
        }
    }
}
