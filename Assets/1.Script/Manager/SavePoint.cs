using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class SavePoint : MonoBehaviour
{
    public GameObject savePanel; //���̺� �г�
    private bool isSavePanelOpen = false; //���̺� �г� Ȱ��ȭ ����
    public float saveLength = 3f; //���̺� ���� ����
    public Text saveTrue; //���̺� ������ ������ ���Դ��� ǥ��
    private bool isInRange = false; //���̺� ���� �������� Ȯ��

    private bool isPaused = false; //������ �Ͻ����� �Ǿ�����

    void Start()
    {

    }

    void Update()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");  //Player �±׸� ���� ������Ʈ ã��

        foreach (GameObject player in players)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position); //�÷��̾�� ���� ��ũ��Ʈ�� ���� ������Ʈ ������ �Ÿ�����

            if (distance <= saveLength) //�Ÿ��� saveLength �̳��� �ִٸ�
            {
                isInRange = true; //���̺� ���� ������ ����
                break;
            }
            else
            {
                isInRange = false; //���̺� ���� �Ÿ� ����
            }
        }

        if (isInRange) //���� ���� �ȿ� �ִٸ�
        {
            Debug.Log("���̺갡��");
            //saveTrue ������Ʈ�� ���İ��� 1�� ����
            Color color = saveTrue.color;
            color.a = 1f;
            saveTrue.color = color;
        }
        else
        {
            Debug.Log("���̺�Ұ���");
            //���� �ۿ� �ִٸ� saveTrue ������Ʈ�� ���İ��� 0���� ����
            Color color = saveTrue.color;
            color.a = 0f;
            saveTrue.color = color;
        }

        saveStart();
    }

    public void saveStart()
    {
        if (isInRange == true) //���̺� ���� �����ȿ� ��������
        {
            if (Input.GetKeyDown(KeyCode.Space)) //�����̽� �Է� ��
            {
                if (isSavePanelOpen == false) //���̺�â�� �����ִٸ�
                {
                    OpenSavePanel(); //���̺�â ����
                }
                else //���̺�â�� �����ִٸ�
                {
                    CloseSavePanel(); //���̺�â �ݱ�
                }
            }
        }
    }

    public void OpenSavePanel() //���̺�â ����
    {
        if (!isPaused)
        {
            PauseGame();
            savePanel.SetActive(true);
            isSavePanelOpen = true;
        }
    }

    public void CloseSavePanel() //���̺�â �ݱ�
    {
        if (isPaused)
        {
            ResumeGame();
            savePanel.SetActive(false);
            isSavePanelOpen = false;
        }
    }

    void PauseGame() //���� �Ͻ�����
    {
        isPaused = true;
        Time.timeScale = 0;
    }

    void ResumeGame() //���� �簳
    {
        isPaused = false;
        Time.timeScale = 1;
    }
}