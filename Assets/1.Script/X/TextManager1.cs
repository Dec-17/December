using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager1 : MonoBehaviour
{
    public Text dialogueText; // ���̾�α� �ؽ�Ʈ
    public GameObject dialogueWindow; // ��ȭâ

    public int currentTextIndex = 0; // ���� �ؽ�Ʈ �ε���
    public float typingSpeed = 0.05f; // Ÿ���� �ӵ�
    private bool isTyping = false; // Ÿ���� ������ ����
    public int chapterNumber = 0; // ���� é�� ��ȣ
    public TextArray[] chapter; // é�� �迭

    private bool[] isChapter; // �� é�� ���� ���θ� �����ϴ� �迭

    // ���� ���� �� �ʱ�ȭ
    public void Start()
    {
        // ��ȭâ UI�� ��Ȱ��ȭ
        dialogueText.gameObject.SetActive(false);
        dialogueWindow.SetActive(false);

        // �� é���� ���� ���θ� ������ �迭�� �ʱ�ȭ
        isChapter = new bool[chapter.Length];

        // �迭�� ��� false�� �ʱ�ȭ
        for (int i = 0; i < isChapter.Length; i++)
        {
            isChapter[i] = false;
        }
    }

    // �� �����Ӹ��� ����Ǵ� ������Ʈ �Լ�
    private void Update()
    {
        // �ؽ�Ʈ �ε� �Լ� ȣ��
        textLoad();
    }

    // �ؽ�Ʈ�� �ε��ϴ� �Լ�
    public void textLoad()
    {
        // �����̽� Ű �Է� ����
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Ÿ���� ���� �ƴ� ��
            if (!isTyping)
            {
                // ���� é���� �ؽ�Ʈ �ε����� �迭 ���̺��� ���� ��
                if (currentTextIndex < chapter[chapterNumber].talk.Length)
                {
                    // Ÿ���� ȿ���� �����ϴ� �ڷ�ƾ ����
                    StartCoroutine(TypeText(chapter[chapterNumber].talk[currentTextIndex]));
                    currentTextIndex++; // �ؽ�Ʈ �ε��� ����

                    // ��ȭâ UI Ȱ��ȭ
                    dialogueText.gameObject.SetActive(true);
                    dialogueWindow.SetActive(true);
                }
                else
                {
                    // ��ȭ�� ����Ǿ��� �� ��ȭâ UI�� ��Ȱ��ȭ
                    dialogueText.gameObject.SetActive(false);
                    dialogueWindow.SetActive(false);
                }
            }
            else // Ÿ���� ���� ��
            {
                // Ÿ���� ȿ���� ��� �����ϰ� ���� �ؽ�Ʈ�� ��� ǥ��
                StopAllCoroutines();
                dialogueText.text = chapter[chapterNumber].talk[currentTextIndex - 1];
                isTyping = false;
            }
        }
        else // ��ȭ�� ���۵��� ���� ������ ��
        {
            if (!isTyping && currentTextIndex == 0)
            {
                // ���� é���� �ؽ�Ʈ �迭�� ���̰� 0���� ũ�� ��ȭ�� ����
                if (chapter[chapterNumber].talk.Length > 0)
                {
                    StartCoroutine(TypeText(chapter[chapterNumber].talk[currentTextIndex]));
                    currentTextIndex++;

                    // ��ȭâ UI Ȱ��ȭ
                    dialogueText.gameObject.SetActive(true);
                    dialogueWindow.SetActive(true);
                }
            }
        }
    }

    // Ÿ���� ȿ���� �����ϴ� �ڷ�ƾ �Լ�
    IEnumerator TypeText(string textToType)
    {
        // �Էµ� �ؽ�Ʈ�� null�� �ƴ� ��
        if (textToType != null)
        {
            isTyping = true; // Ÿ���� ������ ��Ÿ��
            dialogueText.text = ""; // ��ȭ �ؽ�Ʈ �ʱ�ȭ

            // �Էµ� �ؽ�Ʈ�� �� ���ھ� ��ȸ�ϸ鼭 ��ȭâ�� ǥ��
            foreach (char letter in textToType)
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(typingSpeed); // ���� �ð� �������� �ؽ�Ʈ�� ǥ��
            }

            isTyping = false; // Ÿ���� ����
        }
    }

    // é�͸� �����ϴ� �Լ�
    public void Chapter1()
    {
        // �ش� é�Ͱ� ������� �ʾ��� �� ����
        if (!isChapter[1])
        {
            chapterNumber = 1; // ���� é�� ��ȣ ����
            currentTextIndex = 0; // �ؽ�Ʈ �ε��� �ʱ�ȭ
            isChapter[1] = true; // �ش� é�Ͱ� ������� ǥ��
        }
    }
}
