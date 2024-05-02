using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;
using UnityEditor.Rendering;

public class TextManager : MonoBehaviour
{
    private float talkLength = 3f; //��ȭ ���� ����
    public GameObject talkTrue; //��ȭ ������ ������ ���Դ��� ǥ��
    private bool isInRange = false; //��ȭ ���� �������� Ȯ��

    public Text dialogueText; //���̾�α� �ؽ�Ʈ
    public GameObject dialogueWindow; //���̾�α� ��ȭĭ

    private float typingSpeed = 0.1f; //Ÿ���θ�� �ӵ�
    private bool isTyping = false; // Ÿ���� ��� ���� ���� Ȯ��

    public string[] talk; //���̾�α� �ؽ�Ʈ�� �Է� �� �迭
    private int currentTalkIndex = 0; //���� ��� ���� talk�� �ε���
    
    void Start()
    {
        //���� ���� �� ��ȭâ UI ��Ȱ��ȭ
        dialogueText.gameObject.SetActive(false);
        dialogueWindow.SetActive(false);
    }

    void Update()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");  //Player �±׸� ���� ������Ʈ ã��

        foreach (GameObject player in players)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position); //�÷��̾�� ���� ��ũ��Ʈ�� ���� ������Ʈ ������ �Ÿ�����

            if (distance <= talkLength) //�Ÿ��� talkLength �̳��� �ִٸ�
            {
                isInRange = true; //��ȭ ���� ������ ����
                break;
            }
            else
            {
                isInRange = false; //��ȭ ���� �Ÿ� ����
            }
        }

        if (isInRange) //���� ���� �ȿ� �ִٸ�
        {
            Debug.Log("��ȭ����");
            //talkTrue ������Ʈ�� ���İ��� 1�� ����
            Color color = talkTrue.GetComponent<Renderer>().material.color;
            color.a = 1f;
            talkTrue.GetComponent<Renderer>().material.color = color;
        }
        else
        {
            Debug.Log("��ȭ�Ұ���");
            //���� �ۿ� �ִٸ� talkTrue ������Ʈ�� ���İ��� 0���� ����
            Color color = talkTrue.GetComponent<Renderer>().material.color;
            color.a = 0f;
            talkTrue.GetComponent<Renderer>().material.color = color;

            //��ȭ ���� ���� ������ ������ ��ȭ �ʱ�ȭ
            dialogueText.gameObject.SetActive(false);
            dialogueWindow.SetActive(false);
            currentTalkIndex = 0;
        }

        dialogueStart();
    }

    public void dialogueStart()
    {
        if (isInRange == true)
        {
            if (!isTyping && Input.GetKeyDown(KeyCode.Space))
            {
                if (currentTalkIndex < talk.Length)
                {
                    dialogueText.gameObject.SetActive(true); //���̾�α� �ؽ�Ʈ Ȱ��ȭ
                    dialogueWindow.SetActive(true); //��ȭâ Ȱ��ȭ
                    StartCoroutine(TypeText(talk[currentTalkIndex])); //Ÿ���� ��� ����
                }
                else
                {
                    // ��ȭ�� ��� ��µ� ��� ��ȭâ ��Ȱ��ȭ, currentTalkIndex �ʱ�ȭ
                    dialogueText.gameObject.SetActive(false);
                    dialogueWindow.SetActive(false);
                    currentTalkIndex = 0;
                }
            }
        }
    }

    IEnumerator TypeText(string textToType) //Ÿ���� ���
    {
        isTyping = true; //Ÿ���� ������ ����
        dialogueText.text = ""; //��ȭ �ؽ�Ʈ �ʱ�ȭ
        int charCount = 0; //��µ� ���� ��

        foreach (char letter in textToType) //�Էµ� �ؽ�Ʈ�� �� ���ھ� ��ȸ
        {
            dialogueText.text += letter; //�� ���ڸ� ���̾�α� �ؽ�Ʈ�� �ϳ��� �Է�
            charCount++; //�ѱ��ڰ� �Է� �ɶ����� ��µ� ���� �� ++

            if (charCount > 10 && letter == ' ') //10���� �̻� ��µȴٸ�
            {
                dialogueText.text += "\n"; //�ٹٲ�
                charCount = 0; //��µ� ���� �� 
            }
            yield return new WaitForSeconds(typingSpeed); //typingSpeed��ŭ ���
        }

        isTyping = false; //Ÿ���� ����
        currentTalkIndex++; //���� ��ȭ �������� �ε��� ����
    }
}