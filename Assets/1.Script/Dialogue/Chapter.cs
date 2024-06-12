using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter : MonoBehaviour
{
    Dialog dialog;


    void Start()
    {
        dialog = FindObjectOfType<Dialog>();
    }

    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // �÷��̾� �±׿� �浹 ������
        {
            switch (gameObject.name) // �� ��ũ��Ʈ�� ���� ������Ʈ�� �̸���
            {
                case "chapter00": // chapter00 �̶��
                    Debug.Log("é��0 ����");
                    dialog.SetChapterNum(0); // ���̾�α� é�͸� 0������ ����
                    dialog.DialogueStart(); // ��ȭ ����
                    Destroy(gameObject); // ��ȭ�� �ѹ��� ����ǵ��� ������Ʈ �ı�
                    break;
                case "chapter01":
                    Debug.Log("é��1 ����");
                    dialog.SetChapterNum(1);
                    dialog.DialogueStart();
                    Destroy(gameObject);
                    break;
                case "chapter02":
                    Debug.Log("é��2 ����");
                    dialog.SetChapterNum(2);
                    dialog.DialogueStart();
                    Destroy(gameObject);
                    break;
                case "chapter03":
                    Debug.Log("é��3 ����");
                    dialog.SetChapterNum(3);
                    dialog.DialogueStart();
                    Destroy(gameObject);
                    break;
                default:
                    Debug.Log("�� �� ���� chapter");
                    break;
            }
        }
    }
}